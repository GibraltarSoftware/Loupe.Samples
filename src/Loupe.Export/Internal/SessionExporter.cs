using System;
using System.Diagnostics;
using System.IO;
using Gibraltar.Data;
using Gibraltar.Monitor;
using Loupe.Extensibility.Data;

namespace Loupe.Export.Internal
{
    internal class SessionExporter
    {
        public void Export(ExportConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrWhiteSpace(configuration.SourceFileNamePath))
                throw new ArgumentNullException("SourceFileNamePath");

            //See if the local file even exists..
            Console.WriteLine("Opening Source File '{0}'...", configuration.SourceFileNamePath);
            using (var sourceFileStream = File.OpenRead(configuration.SourceFileNamePath))
            {
                if (GLFReader.IsGLF(sourceFileStream) == false)
                    throw new InvalidOperationException("Specified source file is not a Loupe Log file (glf)");

                var sessionCollection = new SessionCollection();
                var session = sessionCollection.Add(sourceFileStream);

                if (session == null)
                    throw new InvalidOperationException("Unable to find session data in source data file");

                Console.WriteLine("Initializing {0} Export...", configuration.LogMessageFormat);
                using (session)
                {
                    //instance the correct exporter...
                    LogMessageFormatter messageFormatter;
                    switch (configuration.LogMessageFormat)
                    {
                        case LogMessageFormat.Default:
                            messageFormatter = new DefaultLogMessageFormatter(configuration);
                            break;
                        case LogMessageFormat.Summary:
                            messageFormatter = new SummaryLogMessageFormatter(configuration);
                            break;
                        case LogMessageFormat.Detailed:
                            messageFormatter = new DetailedLogMessageFormatter(configuration);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    ISession readOnlySession = new ReadOnlySession(session);

                    var stopwatch = Stopwatch.StartNew();
                    Console.WriteLine("Exporting Session Data into '{0}'...", configuration.TargetFileNamePath);

                    messageFormatter.Export(readOnlySession);

                    stopwatch.Stop();
                    Console.WriteLine("Completed export in {0:T}.", stopwatch.Elapsed);
                }
            }
        }
    }
}
