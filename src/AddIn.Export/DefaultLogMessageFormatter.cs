using Gibraltar.Extensibility.Client;
using Gibraltar.Extensibility.Data;

namespace Gibraltar.AddIn.Export
{
    internal class DefaultLogMessageFormatter : LogMessageFormatter
    {
        public DefaultLogMessageFormatter(ExportAddInConfiguration config) : base(config)
        {
        }

        protected override void ExportSummary(IRepositoryExtensionContext context, ISession session)
        {
            var summary = session.Summary;
            if (Config.IncludeSessionSummary)
            {
            Write(
                "=== SESSION SUMMARY =========================================================================================\r\n");
            Write("{0}.{1} v{2} on {3} by {4}\r\n", summary.Product, summary.Application, summary.ApplicationVersion,
                summary.HostName, summary.FullyQualifiedUserName);
            Write("From {0} to {1}  ({2})\r\n", summary.StartDateTime, summary.EndDateTime, summary.Duration);
            Write("{3} Messages: {0} Criticals, {1} Errors, {2} Warnings\r\n", session.CriticalCount,
                session.ErrorCount, session.WarningCount, session.MessageCount);
            Write("App Type: {0}\r\n", summary.ApplicationType);
            Write("Agent Version: {0}\r\n", summary.AgentVersion);
            Write(".NET Runtime Version: {0}\r\n", summary.RuntimeVersion);
            Write("Command Line: {0}\r\n", summary.CommandLine);
            Write("OS Details: {0}\r\n", summary.OSFullNameWithServicePack);
            Write("DNS Domain: {0}\r\n", summary.DnsDomainName);
            Write("Processors: {0}\r\n", summary.Processors);
            Write("Memory: {0}\r\n", summary.MemoryMB);
            Write("OS Boot Modes: {0}\r\n", summary.OSBootMode);
            Write("OS Architecture: {0}\r\n", summary.OSArchitecture);
            Write("Process Architecture: {0}\r\n", summary.RuntimeArchitecture);
            Write("Screen: {0}x{1}x{2}\r\n", summary.ScreenWidth, summary.ScreenHeight, summary.ColorDepth);
            Write("Time Zone: {0}\r\n", summary.TimeZoneCaption);
            Write("User Interactive: {0}\r\n", summary.UserInteractive);
            Write("Terminal Server: {0}\r\n", summary.TerminalServer);
            Write(
                "=== LOG MESSAGES ============================================================================================\r\n");
            }
            else
            {
                Write("{0}.{1} v{2}\r\n", summary.Product, summary.Application, summary.ApplicationVersion);
                Write("Computer: {0}  User: {1}\r\n", summary.HostName, summary.FullyQualifiedUserName);
                Write("Session Start: {0} End: {1}  Duration: {2}\r\n", summary.StartDateTime,
                    summary.EndDateTime, summary.Duration);
                Write("{3} Messages: {0} Criticals, {1} Errors, {2} Warnings\r\n", session.CriticalCount,
                    session.ErrorCount, session.WarningCount, session.MessageCount);
            }
        }

        protected override void ExportLogMessage(IRepositoryExtensionContext context, ISession session, ILogMessage message)
        {
            WriteLine(); 
            if (message.Severity == LogMessageSeverity.Verbose)
                Write("{0} {1}\r\n", message.Timestamp, message.Caption.Trim());
            else
            {
                var severity = message.Severity.ToString();
                if (severity.StartsWith("Info") || severity.StartsWith("Warn"))
                    severity = severity.Substring(0, 4);
                else
                    severity = severity.ToUpperInvariant();

                Write("{0} {2} {1}\r\n", message.Timestamp, message.Caption.Trim(), severity);
            }
            if (string.IsNullOrEmpty(message.Description) == false)
            {
                Write("{0}\r\n", message.Description.Trim());
            }
            Write("Category: {0} from {1}", message.CategoryName, message.LogSystem);

            if (string.IsNullOrEmpty(message.ClassName) == false)
            {
                Write("  {0}.{1}", message.ClassName, message.MethodName);
            }

            if (string.IsNullOrEmpty(message.FileName) == false)
            {
                Write(" in {0} ({1})", message.FileName, message.LineNumber);
            }
            WriteLine();

            Write("Thread: {0} ({1}) for {2}\r\n", message.ThreadName, message.ThreadId, message.UserName);

            IExceptionInfo currentException = message.Exception;
            if (currentException != null)
            {
                if (Config.IncludeExceptionDetails)
                {
                    var label = "EXCEPTION";
                    while (currentException != null)
                    {
                        Write("{0}: {1} - {2} from {3}\r\nStack Trace:\r\n{4}\r\n",
                            label, currentException.TypeName, currentException.Message.Trim(),
                            currentException.Source, currentException.StackTrace);
                        currentException = currentException.InnerException;
                        label = "INNER EXCEPTION";
                    }
                }
                else
                {
                    while (currentException != null)
                    {
                        Write("{0}: {1}\r\n", currentException.TypeName, currentException.Message.Trim());
                        currentException = currentException.InnerException;
                    }
                }
                WriteLine();
            }
        }
    }
}
