using System;
using System.Collections.Generic;
using System.IO;
using Loupe.Extensibility.Client;
using Loupe.Extensibility.Data;
using Loupe.Extensibility.Server;

namespace Loupe.Extension.Sample
{
    public class SessionAnalysisAddInSample : ISessionAnalyzer, ISessionCommand
    {
        private const string LogCategory = "Sample Add In.Session Analysis";

        private IRepositoryContext m_AddInContext;

        private bool m_Initialized;
        private bool m_IsDisposed;
        private bool m_Enabled;
        private string m_OutputPath;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (m_Initialized)
                m_Initialized = false;

            if (m_IsDisposed)
                throw new InvalidOperationException("The add in has already been disposed");

            m_AddInContext = null;
            m_IsDisposed = true;
        }

        /// <summary>
        /// Called to initialize the add in.
        /// </summary>
        /// <remarks>
        /// If any exception is thrown during this call the Add In will not be loaded.
        /// </remarks>
        public void Initialize(IRepositoryContext context)
        {
            if (m_Initialized)
                throw new InvalidOperationException("The add in has already been initialized and shouldn't be re-initialized");

            m_Initialized = true;
            m_AddInContext = context;
            ConfigurationChanged(); //so we load our configuration the first time.
        }

        /// <summary>
        /// Called by Loupe to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
            //we're going to read the config here and save what we care about so we don't have to 
            //deal with error handling during the process phase.

            SampleAddInConfiguration configuration = m_AddInContext.Configuration.Common as SampleAddInConfiguration;

            if (configuration == null)
            {
                //this should never happen.
                m_AddInContext.Log.Warning(LogCategory, "Invalid Configuration Data", "No configuration data could be loaded so the session analysis export will be disabled");

                m_Enabled = false;
                m_OutputPath = null;
            }
            else
            {
                m_Enabled = configuration.AutoExportSessions;
                m_OutputPath = configuration.SessionExportPath ?? SampleAddInConfiguration.DefaultOutputPath;
            }
        }

        void ISessionAnalyzer.Process(ISession session)
        {
            //on this route we only process if we're enabled
            if (m_Enabled == false)
            {
                return;
            }

            ExportSession(session);
        }

        /// <summary>
        /// Analyze the provided session.
        /// </summary>
        /// <param name="session">The session to be analyzed</param>
        private void ExportSession(ISession session)
        {
            //lets have fun writing an entire session out to a text file.
            string filePath = m_OutputPath;
            string fileName = session.Id + ".txt";
            if (Directory.Exists(filePath) == false)
                Directory.CreateDirectory(filePath);

            using(StreamWriter writer = new StreamWriter(Path.Combine(filePath, fileName)))
            {
                writer.Write("SESSION SUMMARY:\r\n================================================================================================\r\n");
                writer.Write("Product: {0} App: {1}\r\n", session.Summary.Product, session.Summary.Application);
                writer.Write("Criticals: {0} Errors: {1} Warnings: {2} Messages: {3}\r\n", session.CriticalCount, session.ErrorCount, session.WarningCount, session.MessageCount);
                writer.Write("Session Start: {0} End: {1}  Duration: {2}\r\n", session.Summary.StartDateTime, session.Summary.EndDateTime, session.Summary.Duration);
                writer.Write("App Description: {0}\r\n", session.Summary.ApplicationDescription);
                writer.Write("App Type: {0}\r\n", session.Summary.ApplicationType);
                writer.Write("App Version: {0}\r\n", session.Summary.ApplicationVersion);
                writer.Write("Agent Version: {0}\r\n", session.Summary.AgentVersion);
                writer.Write("Command Line: {0}\r\n", session.Summary.CommandLine);
                writer.Write("User: {0}\r\n", session.Summary.FullyQualifiedUserName);
                writer.Write("Computer: {0}\r\n", session.Summary.HostName);
                writer.Write("OS Details: {0}\r\n", session.Summary.OSFullNameWithServicePack);
                writer.Write("DNS Domain: {0}\r\n", session.Summary.DnsDomainName);
                writer.Write("Processors: {0}\r\n", session.Summary.Processors);
                writer.Write("Memory: {0}\r\n", session.Summary.MemoryMB);
                writer.Write("OS Boot Modes: {0}\r\n", session.Summary.OSBootMode);
                writer.Write("OS Architecture: {0}\r\n", session.Summary.OSArchitecture);
                writer.Write("Process Architecture: {0}\r\n", session.Summary.RuntimeArchitecture);
                writer.Write(".NET Runtime Version: {0}\r\n", session.Summary.RuntimeVersion);
                writer.Write("Screen: {0}x{1}x{2}\r\n", session.Summary.ScreenWidth, session.Summary.ScreenHeight, session.Summary.ColorDepth);
                writer.Write("Time Zone: {0}\r\n", session.Summary.TimeZoneCaption);
                writer.Write("User Interactive: {0}\r\n", session.Summary.UserInteractive);
                writer.Write("Terminal Server: {0}\r\n", session.Summary.TerminalServer);


                writer.Write("\r\n\r\nTHREADS:\r\n================================================================================================\r\n");
                foreach (IThreadInfo thread in session.Threads)
                {
                    writer.Write("ID: {0} Name: {1} Background: {2} Thread Pool: {3}\r\n", 
                        thread.ThreadId, thread.ThreadName, thread.IsBackground, thread.IsThreadPoolThread);
                }
                writer.Write("================================================================================================\r\n");

                writer.Write("\r\n\r\nASSEMBLIES:\r\n================================================================================================\r\n");
                foreach (IAssemblyInfo assembly in session.Assemblies)
                {
                    writer.Write("Name: {0} Version: {1} File Version: {2} Architecture: {3} GAC: {4} Culture: {6}\r\n  Loaded at: {7}\r\n  Location: {5}\r\n",
                        assembly.Name, assembly.Version, assembly.FileVersion, assembly.ProcessorArchitecture, assembly.GlobalAssemblyCache, assembly.Location, assembly.CultureName, assembly.LoadedTimeStamp);
                }
                writer.Write("================================================================================================\r\n");

                writer.Write("\r\n\r\nMESSAGES:\r\n================================================================================================\r\n");
                foreach (ILogMessage message in session.GetMessages())
                {
                    writer.Write("\r\n{1} {2} (Sequence {0}) Logged from: {3}\r\nCategory: {4}\r\n", 
                        message.Sequence, message.Timestamp, message.Severity.ToString().ToUpperInvariant(), message.LogSystem, message.CategoryName);

                    if (string.IsNullOrEmpty(message.ClassName) == false)
                    {
                        writer.Write("Class: {0} Method: {1}\r\n", message.ClassName, message.MethodName);
                    }

                    if (string.IsNullOrEmpty(message.FileName) == false)
                    {
                        writer.Write("File: {0} Line: {1}\r\n", message.FileName, message.LineNumber);
                    }

                    writer.Write("Thread Id: {0} Name: {1}\r\n", message.ThreadId, message.ThreadName);
                    writer.Write("User: {0}\r\n", message.UserName);


                    writer.Write("Caption: {0}\r\n", message.Caption);

                    if (string.IsNullOrEmpty(message.Description) == false)
                    {
                        writer.Write("{0}\r\n", message.Description);
                    }

                    writer.WriteLine();

                    IExceptionInfo currentException = message.Exception;
                    if (currentException != null)
                    {
                        writer.Write("Exceptions:\r\n------------------------------------------------------------------------------------------------\r\n");
                        while (currentException != null)
                        {
                            writer.Write("Exception: {0}\r\nMessage: {1}\r\nSource: {2}\r\nStack Trace:\r\n{3}\r\n",
                                         currentException.TypeName, currentException.Message, currentException.Source, currentException.StackTrace);
                            currentException = currentException.InnerException;
                        }
                        writer.Write("------------------------------------------------------------------------------------------------\r\n");
                    }

                    if (string.IsNullOrEmpty(message.Details) == false)
                    {
                        writer.Write("Exceptions:\r\n------------------------------------------------------------------------------------------------\r\n");
                        writer.Write(message.Details);
                        writer.Write("------------------------------------------------------------------------------------------------\r\n");
                    }
                }
                writer.Write("================================================================================================\r\n");
            }
        }

        /// <summary>
        /// Called to have the session command object register all commands that it supports.
        /// </summary>
        /// <param name="controller"/>
        public void RegisterCommands(IUserInterfaceContext controller)
        {
            controller.RegisterCommand("export", false, "Save Session as Text", "Exports the session as a text file in all of its detail");
            controller.RegisterCommand("openSession", true, "Custom Command Open Session", null);
            controller.RegisterCommand("invisible", true, "Invisible", null);
        }


        /// <summary>
        /// Called just prior to displaying the list of commands to a user on a set of sessions.
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param><param name="sessionSummaries">Summaries of the selected sessions.</param>
        /// <remarks>
        /// <para>
        /// Use this method to change what commands are available or what their labels should be prior to being displayed.
        /// </para>
        /// <para>
        /// The controller should not be persisted or accessed between calls, it may change and 
        ///             the same object may get calls from multiple controllers.
        /// </para>
        /// </remarks>
        public void BeforeCommandsDisplay(IUserInterfaceContext controller, IList<ISessionSummary> sessionSummaries)
        {
            controller.UpdateCommand("export", "Save Session as Text", "Exports the session as a text file in all of its detail", true);

            //now we only want to enable our custom open command if there's exactly one session enabled.
            controller.UpdateCommand("openSession", sessionSummaries.Count == 1);

            controller.UpdateCommand("invisible", false);
        }


        /// <summary>
        /// Apply the specified command to the provided set of sessions
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param><param name="commandName">The command that was requested.</param><param name="sessionSummaries">Summaries of the selected sessions.</param>
        /// <remarks>
        /// <para>
        /// Headers of all of the currently selected sessions are provided.  If the command
        ///             was configured to provide its own user interface it will be called on the main UI thread and needs to perform
        ///             its own background processing to keep the user interface responsive.  If not, it will be called
        ///             from a background thread and the user interface will be kept responsive by the framework.
        /// </para>
        /// <para>
        /// The controller should not be persisted or accessed between calls, it may change and 
        ///             the same object may get calls from multiple controllers.
        /// </para>
        /// </remarks>
        public void Process(IUserInterfaceContext controller, string commandName, IList<ISessionSummary> sessionSummaries)
        {
            switch (commandName)
            {
                case "export":
                    foreach (ISessionSummary summary in sessionSummaries)
                    {
                        ExportSession(summary.Session());
                    }
                    break;
                case "openSession":
                    m_AddInContext.DisplaySession(sessionSummaries[0].Id, null);
                    break;
            }
        }
    }
}
