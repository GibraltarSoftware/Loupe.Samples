#region File Header

// /********************************************************************
//  * COPYRIGHT:
//  *    This software program is furnished to the user under license
//  *    by eSymmetrix, Inc, and use thereof is subject to applicable 
//  *    U.S. and international law. This software program may not be 
//  *    reproduced, transmitted, or disclosed to third parties, in 
//  *    whole or in part, in any form or by any manner, electronic or
//  *    mechanical, without the express written consent of eSymmetrix, Inc,
//  *    except to the extent provided for by applicable license.
//  *
//  *    Copyright © 2008 by eSymmetrix, Inc.  All rights reserved.
//  *******************************************************************/

using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Gibraltar.Agent;

#endregion

namespace Gibraltar.TraceMonitorSamples
{
    /// <summary>
    /// A sample application using static binding to Gibraltar.Agent.dll as a direct reference.
    /// </summary>
    /// <remarks><para>
    /// This example is compiled with direct reference to Gibraltar.  This allows access to the full
    /// features of the Gibraltar API, including custom metrics (not shown here).
    /// </para>
    /// <para>
    /// This example shows a typical winforms Program.Main() with a few recommended calls to make sure the
    /// Gibraltar Agent can perform at its best.  It also shows an example of how your existing exception handling
    /// can use the Gibraltar API to invoke the Gibraltar Error Manager (or to just record the exception in the
    /// session log) rather than use the Agent's handler.
    /// </para>
    /// <para>
    /// Also see the dynamic binding sample application as an alternative approach showing how to attach the
    /// Gibraltar Agent without a direct reference.
    /// </para></remarks>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // This example does everything without an app.config file, even configures the Agent.  
            // To do that, we have to register for the configuration event before we do anything that uses the agent.
            Log.Initializing += Log_Initializing;

            // This next line is an example of how to programatically attach Gibraltar as a Trace Listener
            // rather than do it in an app.config file.

            Trace.Listeners.Add(new LogListener()); // Add Gibraltar.Agent.LogListener as a Trace Listener (no app.config)...
            Trace.TraceInformation("Application starting."); // ...so the Gibraltar Agent will receive this message and initialize.

            // OR... You could initialize the Gibraltar Agent by just invoking the Agent directly. (You don't really need both.)
            Log.StartSession("Application starting."); // This message only goes to Gibraltar, not to other Trace listeners.

            // This example adds its own exception event handling (optional usage).
            // Also see the dynamic-binding example which uses only Gibraltar's automatic handling.

            Application.ThreadException += Application_ThreadException; // This will replace the Agent's handler with this one; only the last subscriber is kept.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException); // Make sure the message loop catches exceptions for the event.
            Application.ApplicationExit += Application_ApplicationExit; // Recommended by MSDN to detach handlers on exit.
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; // Optionally add our own handler for unhandled exceptions.

            // Finally, launch the main application form.
            Application.Run(new MainApp());

            Log.EndSession("Application exiting."); // Tell Gibraltar directly that we're exiting.

            // Or...
            Trace.TraceInformation("Application exiting."); // An exiting message just for completeness with the initial Trace message.
            Trace.Close(); // This also tells Gibraltar indirectly that we're exiting.
            // It's a good idea to close Trace when you're exiting to have every listener shut down nicely.
        }

        static void Log_Initializing(object sender, LogInitializingEventArgs e)
        {
            // Here we can configure and even cancel initialization of the application.

            // You can use the publisher configuration to override the product & application name
            e.Configuration.Publisher.ProductName = "Override Product";
            e.Configuration.Publisher.ApplicationName = "Override Application";

            // You can also add name/value pairs to be recorded in the session header for improved
            // categorization and analysis later.
            e.Configuration.Properties.Add("CustomerName", "The customer name from our license");

            // You can also use the cancel feature combined with #define to turn off the agent at compile time.
#if (!DEBUG)
            // This is not a debug compile, and we'll choose to disable the agent for release builds.
            e.Cancel = true;
#endif
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            // MSDN says we should make sure to detach our handlers on exit because these are static events.
            Application.ApplicationExit -= Application_ApplicationExit;
            Application.ThreadException -= Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // It isn't necessary to report these exceptions from your own handler because the Agent always gets these
            // events and records them automatically (unless you configure Gibraltar to ignore them by setting
            // catchUnhandledExceptions="false" in the listener section of the gibraltar group in your app.config,
            // or through the Log.Initializing event available with static binding; see the help documentation for more info).
            // So you only have to do any other handling your application needs to do before the application is killed.
            // (AppDomain.UnhandledException events are ALWYAS fatal, as of .NET 2.0)

            Exception exception = e.ExceptionObject as Exception; // This could also get non-CLR exceptions (no stack trace available).
            if (exception == null)
                exception = new Exception("Unwrapped exception: " + e.ExceptionObject); // We'll just put the object's ToString() as the message.

            // If you want to format your own log message (perhaps with further status from your handler), you can include the
            // full exception data (including stack trace).  Here's how you can do it (from anywhere) with the Gibraltar API:

            Log.Critical(exception, LogWriteMode.WaitForCommit, "Exception.Unhandled", "Unhandled exception event",
                "This description could include further details or messages from your handler logic, including string format args.  "+
                "The full type, message, and stack trace of the exception are all attached already, but you can also include them "+
                "in your formatted message if you wish, in order to make them also available in the Gibraltar Live Viewer.\r\n"+
                "Exception: {0}\r\nMessage: {1}\r\n", exception.GetType().Name, exception.Message);
        }
            
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;

            // Since only one subscriber can receive this event (per UI thread), this is in place of the Agent's handler.

            // Optionally we can log our own formatted message with the exception attached.
            Log.Error(exception, "Exception.Thread.UI", "Application thread exception",
                "This description could include further details or messages.\r\nException: {0}\r\nMessage: {1}\r\n", exception.GetType().Name, exception.Message);

            // Although we've written our own handler, we can still get the Gibraltar Error Manager dialog with this explicit call.
            // This will report the error to the user via the Gibraltar Error Manager dialog, just as it would with the Agent's own handler.
            Log.ReportException(exception, "Exception.Application", true, false); // This will also record an automatic log message with the exception.
        }

    }
}