using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Gibraltar.Agent;

namespace AgentTest.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Log.ReportException(e.Exception, "Test Application", true, false);
            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Lets connect to the Log Initializaing event so we can do programmatic configuration.
            //You can also just use your app.config file, but many people preferr the more powerful approach
            Log.Initializing += Log_Initializing;
            Log.MessageAlert += Log_MessageAlert;

            //Make sure the Gibraltar Agent is spun up right now (we're the entry point to the app)
            Log.StartSession("Starting WPF Sample");

            //just like in WinForms we need to add some visual style for the Gibraltar WinForms dialogs to look nice.
            System.Windows.Forms.Application.EnableVisualStyles();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //This call ensures that the Agent will prepare to exit and close out any open windows.
            Log.EndSession("Ending WPF Sample Normally");
        }

        void Log_MessageAlert(object sender, LogMessageAlertEventArgs e)
        {
            //here we can react to warnings/errors/critical and do an immediate send.
            if (e.TopSeverity <= LogMessageSeverity.Error)
            {
                e.SendSession = true;
                e.MinimumDelay = new TimeSpan(0, 5, 0); //wait a minimum of 5 minutes before alerting us again.
            }
        }

        void Log_Initializing(object sender, LogInitializingEventArgs e)
        {
            e.Configuration.Viewer.FormTitleText = "Live Log Viewer (WinForms within WPF)";
        }
    }
}
