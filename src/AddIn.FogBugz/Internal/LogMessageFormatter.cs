using System.Text;
using Loupe.Extensibility.Data;

namespace Loupe.Extension.FogBugz.Internal
{
    /// <summary>
    /// This class is all about text formatting the error info to be written to FogBugz.
    /// </summary>
    internal class LogMessageFormatter
    {
        public const string SessionIdPrefix = "Loupe Session Id: ";
        public ILogMessage Message { get; private set; }
        public string SessionId { get { return Message.Session.Id.ToString("N"); } }

        public LogMessageFormatter(ILogMessage message)
        {
            Message = message;
        }

        public string GetNewEvent(ErrorInfo error)
        {
            StringBuilder builder = new StringBuilder();

            ILogMessage message = error.Messages[0];
            builder.AppendFormat("New {0} message at {1} {2}\r\n",
                message.Severity,
                message.Timestamp.ToString("d"),
                message.Timestamp.ToString("T"));

            builder.Append(GetSessionDetails());
            if (error.Messages.Count > 1)
            {
                builder.AppendFormat("\r\nDetails below are for the first of {0} occurrences in this session:\r\n\r\n",
                                     error.Messages.Count);
            }

            builder.Append(GetLogMessageDetails());
            builder.AppendFormat("{0}\r\n", GetSessionIdMessage());

            return builder.ToString();
        }

        public string GetRecurrenceLaterVersionEvent(ErrorInfo error)
        {
            StringBuilder builder = new StringBuilder();

            ILogMessage message = error.Messages[0];
            builder.AppendFormat("Recurrent {0} message at {1} {2}",
                message.Severity,
                message.Timestamp.ToString("d"),
                message.Timestamp.ToString("T"));

            builder.Append(GetSessionSummary());
            builder.AppendLine();
            if (error.Messages.Count > 1)
            {
                builder.AppendFormat("Details below are for the first of {0} occurrences in this session:\r\n:",
                                     error.Messages.Count);
            }

            builder.Append(GetLogMessageSummary());
            builder.AppendFormat("\r\n{0}\r\n", GetSessionIdMessage());

            return builder.ToString();
        }

        public string GetRecurrenceSameVersionEvent(ErrorInfo error)
        {
            StringBuilder builder = new StringBuilder();

            ILogMessage message = error.Messages[0];
            if (error.Messages.Count > 1)
                builder.AppendFormat("Another {0} {1} occurrences starting at {2} {3}\r\n",
                                 error.Messages.Count,
                                 message.Severity,
                                 message.Timestamp.ToString("d"),
                                 message.Timestamp.ToString("T"));
            else
                builder.AppendFormat("Another {0} occurrence starting at {1} {2}\r\n",
                                 message.Severity,
                                 message.Timestamp.ToString("d"),
                                 message.Timestamp.ToString("T"));

            builder.AppendFormat("{0}\r\n", GetSessionIdMessage());

            return builder.ToString();
        }

        public string GetSessionIdMessage()
        {
            return SessionIdPrefix + Message.Session.Id.ToString("D");
        }

        public string GetTitle()
        {
            const int MaxTitleLength = 128;
            string severity = Message.Severity.ToString().ToUpperInvariant();
            string caption = Message.Caption ?? string.Empty; //it would be just deadly if this was null
            string className = Message.ClassName;
            string title;

            if (string.IsNullOrEmpty(className))
            {
                title = string.Format("{0}: {1}", severity, caption);
                if (title.Length > MaxTitleLength)
                {
                    int maxLength = MaxTitleLength - severity.Length - 5; // allow for ": and ..."
                    caption = caption.Substring(0, maxLength);
                    title = string.Format("{0}: {1}...", severity, caption);
                }
            }
            else
            {
                title = string.Format("{0}: {1} in {2}", severity, caption, className);
                if (title.Length > MaxTitleLength)
                {
                    string[] parts = className.Split('.');
                    className = parts[parts.GetUpperBound(0)];
                    if (className.Length > MaxTitleLength / 2)
                    {
                        title = string.Format("{0}: {1}", severity, caption);
                        if (title.Length > MaxTitleLength)
                        {
                            int maxLength = MaxTitleLength - severity.Length - 5; // allow for ": and ..."
                            if (caption.Length > maxLength)
                                caption = caption.Substring(0, maxLength);
                            title = string.Format("{0}: {1}...", severity, caption);
                        }
                        return title;
                    }
                    else
                    {
                        int maxLength = MaxTitleLength - severity.Length - className.Length - 9; // allow for ": ", " in " and ...
                        if (caption.Length > maxLength)
                            caption = caption.Substring(0, maxLength);
                        title = string.Format("{0}: {1}... in {2}", severity, caption, className);
                    }
                }
            }
            return title;
        }

        public string GetLogMessageSummary()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("{0}  ", Message.Timestamp.ToString("HH:mm:ss.fff"));

            if (Message.Severity < LogMessageSeverity.Verbose)
                builder.AppendFormat("{0}  ", Message.Severity.ToString().ToUpper());

            string className = Message.ClassName;

            if (!string.IsNullOrEmpty(className))
            {
                string[] parts = className.Split('.');
                className = parts[parts.GetUpperBound(0)];
                builder.AppendFormat("in {0}.{1}\r\n", className, Message.MethodName);
            }

            builder.AppendFormat("{0}\r\n", Message.Caption);

            return builder.ToString();
        }

        public string GetLogMessageDetails()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("{0}", GetLogMessageSummary());

            if (!string.IsNullOrEmpty(Message.Description))
                builder.AppendFormat("{0}\r\n", Message.Description);

            if (!string.IsNullOrEmpty(Message.Details))
            {
                builder.AppendFormat("\r\nAdditional Details:\r\n");
                AppendDivider(builder);
                builder.AppendFormat("{0}\r\n", Message.Details);
                AppendDivider(builder);
            }

            // Include message source info when present, but only for warnings, errors and critical messages
            if (!string.IsNullOrEmpty(Message.CategoryName))
                builder.AppendFormat("Category: {0}\r\n", Message.CategoryName);

            if (!string.IsNullOrEmpty(Message.ClassName))
                builder.AppendFormat("In: {0}.{1}\r\n", Message.ClassName, Message.MethodName);

            if (!string.IsNullOrEmpty(Message.FileName))
                builder.AppendFormat("At: Line {0}\r\n" +
                                     "{1}\r\n",
                                     Message.LineNumber,
                                     Message.FileName);

            if (Message.Exception != null)
            {
                IExceptionInfo exception = Message.Exception;
                if (exception != null)
                {
                    builder.AppendFormat("\r\nException Details:\r\n");
                    AppendDivider(builder);
                    while (exception != null)
                    {
                        builder.AppendFormat("EXCEPTION: {0}\r\n" +
                                             "Source: {1}\r\n" +
                                             "Message: {2}\r\n" +
                                             "Stack Trace:\r\n{3}\r\n\r\n",
                                             exception.TypeName,
                                             exception.Source,
                                             exception.Message,
                                             exception.StackTrace);
                        exception = exception.InnerException;
                    }
                    AppendDivider(builder);
                }
            }

            return builder.ToString();
        }

        public string GetSessionSummary()
        {
            StringBuilder builder = new StringBuilder();
            ISession session = Message.Session;

            builder.AppendFormat("Application:        {0} {1} v{2}\r\n",
                                 session.Summary.Product,
                                 session.Summary.Application,
                                 session.Summary.ApplicationVersion);

            if (string.IsNullOrEmpty(session.Summary.DnsDomainName))
            {
                builder.AppendFormat("Computer:         {0}\r\n",
                                     session.Summary.HostName);
                
            }
            else
            {
                builder.AppendFormat("Computer:         {0}.{1}\r\n",
                                     session.Summary.HostName, 
                                     session.Summary.DnsDomainName);                
            }

            builder.AppendFormat("Username:          {0}\r\n",
                                 session.Summary.FullyQualifiedUserName);

            builder.AppendFormat("Start time:          {0}\r\n" +
                                 "Timezone:           {1}\r\n",
                                 session.Summary.StartDateTime.ToString("g"),
                                 session.Summary.TimeZoneCaption);

            builder.AppendFormat("Culture info:        OS: {0}  Current: {1}   UI: {2}\r\n",
                                 session.Summary.OSCultureName,
                                 session.Summary.CurrentCultureName,
                                 session.Summary.CurrentUICultureName);

            return builder.ToString();
        }

        public string GetSessionDetails()
        {
            StringBuilder builder = new StringBuilder();
            ISession session = Message.Session;

            builder.Append(GetSessionSummary());

            builder.AppendFormat("OS:                    {0}\r\n",
                                 session.Summary.OSFullNameWithServicePack);

            builder.AppendFormat("Architecture:        OS: {0}     Process: {1}\r\n",
                                 session.Summary.OSArchitecture,
                                 session.Summary.RuntimeArchitecture);

            builder.AppendFormat("Framework:        .NET: v{0}   Loupe Agent v{1}\r\n",
                                 session.Summary.RuntimeVersion,
                                 session.Summary.AgentVersion);

            builder.AppendFormat("Hardware:           {0} cores with {1} MB memory\r\n",
                                 session.Summary.ProcessorCores,
                                 session.Summary.MemoryMB);

            builder.AppendFormat("Screen:               {0}x{1}x{2}bits\r\n",
                                 session.Summary.ScreenWidth,
                                 session.Summary.ScreenHeight,
                                 session.Summary.ColorDepth);

            builder.AppendLine();
            AppendDivider(builder);

            return builder.ToString();
        }

        public static void AppendDivider(StringBuilder builder)
        {
            builder.AppendFormat("------------------------------------------------------------------------------\r\n");
        }

        public string GetTags()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Gibraltar");
            return builder.ToString();
        }

    }
}
