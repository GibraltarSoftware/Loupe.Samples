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
using System.Windows.Forms;

#endregion

namespace Gibraltar.TraceMonitorSamples
{
    /// <summary>
    /// A sample application using dynamic binding to Gibraltar.Agent.dll via app.config.
    /// </summary>
    /// <remarks><para>
    /// This example is compiled without direct reference to Gibraltar, only adding Gibraltar's TraceListener
    /// (Gibraltar.Agent.LogListener) in the trace section of the system.diagnostics group in the app.config file to
    /// connect the Agent through the use of built-in Trace logging.  This allows the Gibraltar Agent to be attached to
    /// an application which is already using Trace or other logging systems without recompiling your application.
    /// </para>
    /// <para>
    /// This example shows a typical winforms Program.Main() with a few recommended calls to make sure the
    /// Gibraltar Agent can perform at its best.
    /// </para>
    /// <para>
    /// Also see the static binding sample application as an alternative approach showing some of the features of the
    /// Gibraltar API.</para></remarks>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Fire off a message so Trace will scan the app.config (see also) and add Gibraltar's TraceListener.
            // This ensures that the Gibraltar Agent is loaded and activated, scanning its own part of app.config.
            // Without a message at this point, the Agent would only get loaded when a message is logged some time later,
            // and some of the Agent's automatic features would not be able to function at their best for you.

            Trace.TraceInformation("Application starting.");

            // Nothing else is needed to activate exception handling with Gibraltar on most winforms apps, 
            // as soon as the first line of logging returns above it's active.

            Application.Run(new MainApp());

            Trace.TraceInformation("Application exiting."); // Just for completeness.
            Trace.Close(); // It's always a good idea to close trace when you're exiting the application to have every listener shut down nicely.
        }
    }
}