using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gibraltar.Monitor;
using Loupe.Export.Internal;
using Loupe.Extensibility.Data;

namespace Loupe.Export
{
    public class Program
    {
        private const string LogCategory = "Loupe.Export";

        private const string HelpFileArg = "help";
        private const string HelpFileArgShort = "h";
        private const string SourceFileArg = "source";
        private const string SourceFileArgShort = "s";
        private const string DestinationFileArg = "dest";
        private const string DestinationFileArgShort = "d";
        private const string FormatFileArg = "format";
        private const string FormatFileArgShort = "f";

        static int Main(string[] args)
        {
            try
            {
                //Since we're referencing *internal* Loupe assemblies the Agent API looks pretty different, and we
                //have to do a few things explicitly.
                Log.StartSession(null, 0, "Starting Application");

                //parse input arguments to figure out what they meant.
                var arguments = new Arguments(args);

                var config = new ExportConfiguration();

                //gather our input and verify it
                try
                {
                    config.SourceFileNamePath = FindArg(SourceFileArgShort, SourceFileArg, arguments, true);
                    config.TargetFileNamePath = FindArg(DestinationFileArgShort, DestinationFileArg, arguments, false);

                    string format = FindArg(FormatFileArgShort, FormatFileArg, arguments, false);
                    if (string.IsNullOrWhiteSpace(format) == false)
                    {
                        config.LogMessageFormat = (LogMessageFormat) Enum.Parse(typeof(LogMessageFormat), format, true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    WriteHelpText();
                    return -1;
                }

                //massage input to enforce defaults
                try
                {
                    if (string.IsNullOrWhiteSpace(config.TargetFileNamePath))
                    {
                        //just strip off the file extension and replace it with txt
                        config.TargetFileNamePath = Path.ChangeExtension(config.SourceFileNamePath, "txt");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -2;
                }

                try
                {
                    var exporter = new SessionExporter();
                    exporter.Export(config);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -3;
                }

                return 0;
            }
            catch (Exception ex)
            {
                Log.RecordException(0, ex, null, LogCategory, false);
                Console.WriteLine(ex);
                return -100;
            }
            finally
            {
                Log.EndSession(SessionStatus.Normal, 0, "Exiting");
            }
        }

        private static string FindArg(string shortName, string longName, Arguments args, bool required)
        {
            var value = args[longName];
            if (string.IsNullOrWhiteSpace(value))
            {
                value = args[shortName];
            }

            if (required && string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException(string.Format("No {0} specified", longName));

            return value;
        }

        private static void WriteHelpText()
        {
            Console.WriteLine("Arguments:");
            Console.WriteLine("  -{0} -{1}  Display this help (also when no options given)\r\n", HelpFileArgShort, HelpFileArg);
            Console.WriteLine("  -{0} -{1}  File name of source Loupe data file (GLF),\r\n" +
                              "             relative to current directory or fully qualified path", SourceFileArgShort, SourceFileArg);
            Console.WriteLine("  -{0} -{1}  Optional. File name to write exported log data to,\r\n" +
                              "             relative to current directory or fully qualified path", DestinationFileArgShort, DestinationFileArg);
            Console.WriteLine("  -{0} -{1}  Optional. Output Mode:\r\n" +
                              "              {2}\r\n" +
                              "              {3}\r\n" +
                              "              {4}", FormatFileArgShort, FormatFileArg, LogMessageFormat.Default, LogMessageFormat.Detailed, LogMessageFormat.Summary);
        }
    }
}
