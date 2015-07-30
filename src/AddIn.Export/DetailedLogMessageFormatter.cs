using Gibraltar.Extensibility.Client;
using Gibraltar.Extensibility.Data;

namespace Gibraltar.AddIn.Export
{
    internal class DetailedLogMessageFormatter : LogMessageFormatter
    {
        public DetailedLogMessageFormatter(ExportAddInConfiguration config) : base(config)
        {
        }

        protected override void ExportSummary(IRepositoryExtensionContext context, ISession session)
        {
            var summary = session.Summary;
            if (Config.IncludeSessionSummary)
            {
                Write(
                    "SESSION SUMMARY:\r\n================================================================================================\r\n");
                Write("{0}.{1} v{2}\r\n", summary.Product, summary.Application, summary.ApplicationVersion);
                Write("Computer: {0}  User: {1}\r\n", summary.HostName, summary.FullyQualifiedUserName);
                Write("Session Start: {0} End: {1}  Duration: {2}\r\n", summary.StartDateTime,
                    summary.EndDateTime, summary.Duration);
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
                    "\r\nTHREADS:\r\n================================================================================================\r\n");
                foreach (var thread in session.Threads)
                {
                    Write("ID: {0} Name: {1} Background: {2} Thread Pool: {3}\r\n",
                        thread.ThreadId, thread.ThreadName, thread.IsBackground, thread.IsThreadPoolThread);
                }

                Write(
                    "\r\nASSEMBLIES:\r\n================================================================================================\r\n");
                foreach (var assembly in session.Assemblies)
                {
                    Write(
                        "Name: {0} Version: {1} File Version: {2} Architecture: {3} GAC: {4} Culture: {6}\r\n  Loaded at: {7}\r\n  Location: {5}\r\n",
                        assembly.Name, assembly.Version, assembly.FileVersion, assembly.ProcessorArchitecture,
                        assembly.GlobalAssemblyCache, assembly.Location, assembly.CultureName, assembly.LoadedTimeStamp);
                }

                Write(
                    "\r\nMESSAGES:\r\n================================================================================================\r\n");
            }
            else
            {
                Write("{0}.{1} v{2}\r\n", summary.Product, summary.Application, summary.ApplicationVersion);
                Write("Computer: {0}  User: {1}\r\n", summary.HostName, summary.FullyQualifiedUserName);
                Write("Session Start: {0} End: {1}  Duration: {2}\r\n", summary.StartDateTime,
                    summary.EndDateTime, summary.Duration);
                Write("{3} Messages: {0} Criticals, {1} Errors, {2} Warnings\r\n", session.CriticalCount,
                    session.ErrorCount, session.WarningCount, session.MessageCount);
                WriteLine();                
            }
        }

        protected override void ExportLogMessage(IRepositoryExtensionContext context, ISession session, ILogMessage message)
        {
            WriteLine();
            if (message.Severity == LogMessageSeverity.Verbose)
                Write("{1} (Seq# {0}) Logged from: {2}  Category: {3}\r\n",
                    message.Sequence, message.Timestamp, message.LogSystem, message.CategoryName);
            else
            {
                var severity = message.Severity.ToString();
                if (severity.StartsWith("Info") || severity.StartsWith("Warn"))
                    severity = severity.Substring(0, 4);
                else
                    severity = severity.ToUpperInvariant();

                Write("\r\n{1} {4} (Seq# {0}) Logged from: {2}\r\nCategory: {3}\r\n",
                    message.Sequence, message.Timestamp, message.LogSystem, message.CategoryName, severity);
            }

            if (string.IsNullOrEmpty(message.ClassName) == false)
            {
                Write("Class: {0} Method: {1}\r\n", message.ClassName, message.MethodName);
            }

            if (string.IsNullOrEmpty(message.FileName) == false)
            {
                Write("File: {0} Line: {1}\r\n", message.FileName, message.LineNumber);
            }

            Write("Thread Id: {0} Name: {1}  User: {2}\r\n", message.ThreadId, message.ThreadName, message.UserName);

            Write("Caption: {0}\r\n", message.Caption.Trim());

            if (string.IsNullOrEmpty(message.Description) == false)
            {
                Write("{0}\r\n", message.Description.Trim());
            }

            if (string.IsNullOrEmpty(message.Details) == false)
            {
                Write(
                    "Details:\r\n------------------------------------------------------------------------------------------------\r\n");
                Write(message.Details.Trim());
                Write(
                    "\r\n------------------------------------------------------------------------------------------------\r\n");
            }

            IExceptionInfo currentException = message.Exception;
            if (currentException != null)
            {
                if (Config.IncludeExceptionDetails)
                {
                    Write(
                        "Exceptions:\r\n------------------------------------------------------------------------------------------------\r\n");
                    var label = "EXCEPTION"; 
                    while (currentException != null)
                    {
                        Write("{0}: {1}\r\nMessage: {2}\r\nSource: {3}\r\nStack Trace:\r\n{4}\r\n",
                            label, currentException.TypeName, currentException.Message.Trim(),
                            currentException.Source, currentException.StackTrace);
                        currentException = currentException.InnerException;
                        label = "INNER EXCEPTION";
                    }
                    if (currentException != null)
                        Write(
                            "------------------------------------------------------------------------------------------------\r\n");
                }
                else
                {
                    var label = "EXCEPTION";
                    while (currentException != null)
                    {
                        Write("{0}: {1} - {2}\r\n", label, currentException.TypeName, currentException.Message.Trim());
                        currentException = currentException.InnerException;
                        label = "INNER EXCEPTION";
                    }
                }
            }

            Write(
                "================================================================================================\r\n");

        }
    }
}
