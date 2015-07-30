using System;
using System.IO;
using Gibraltar.Extensibility.Client;
using Gibraltar.Extensibility.Data;

namespace Gibraltar.AddIn.Export
{
    abstract class LogMessageFormatter
    {
        private const string LogCategory = "SessionExport.Export";
        private StreamWriter Writer { get; set; }
        protected ExportAddInConfiguration Config { get; set; }

        protected LogMessageFormatter(ExportAddInConfiguration config)
        {
            Config = config;
        }

        public void Export(IRepositoryExtensionContext context, ISession session)
        {
            if (!Config.EnableLogMessageExport)
                return;

            try
            {
                using (var writer = StreamCreator.CreateLogStream(context, Config, session))
                {
                    Writer = writer;
                    ExportSummary(context, session);
                    
                    foreach (var message in session.GetMessages())
                    {
                        if (message.Severity > Config.MinimumSeverity)
                            continue;

                        ExportLogMessage(context, session, message);
                    }
                }
            }
            catch (Exception ex)
            {
                context.Log.RecordException(ex, LogCategory, true);
            }

        }

        protected abstract void ExportSummary(IRepositoryExtensionContext context, ISession session);
        protected abstract void ExportLogMessage(IRepositoryExtensionContext context, ISession session, ILogMessage message);

        // These are the only methods derived classes need for writing

        protected void Write(string text)
        {
            Writer.Write(text);
        }
        protected void Write(string format, params object[] args)
        {
            Writer.Write(format, args);
        }

        protected void WriteLine()
        {
            Writer.WriteLine();
        }
    }
}
