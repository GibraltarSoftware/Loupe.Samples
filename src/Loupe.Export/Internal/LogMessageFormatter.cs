using System;
using System.IO;
using Gibraltar.Monitor;
using Loupe.Extensibility.Data;

namespace Loupe.Export.Internal
{
    abstract class LogMessageFormatter
    {
        private const string LogCategory = "SessionExport.Export";
        private StreamWriter Writer { get; set; }
        protected ExportConfiguration Config { get; set; }

        protected LogMessageFormatter(ExportConfiguration config)
        {
            Config = config;
        }

        public void Export(ISession session)
        {
            if (!Config.EnableLogMessageExport)
                return;

            try
            {
                using (var writer = StreamCreator.CreateStream(Config.TargetFileNamePath))
                {
                    Writer = writer;
                    ExportSummary(session);
                    
                    foreach (var message in session.GetMessages())
                    {
                        if (message.Severity > Config.MinimumSeverity)
                            continue;

                        ExportLogMessage(session, message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.RecordException(0, ex, null, LogCategory, true);
            }

        }

        protected abstract void ExportSummary(ISession session);
        protected abstract void ExportLogMessage(ISession session, ILogMessage message);

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
