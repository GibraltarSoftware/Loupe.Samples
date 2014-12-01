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

#endregion

#region Using Statements

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace Gibraltar.TraceMonitorSamples
{
    public partial class WorkerUI : UserControl
    {
        private bool m_KeepRunning;
        private Thread m_WorkerThread;
        private int m_WorkerThreadSpeed;
        private int m_WorkerWaitTime;
        private int m_WorkMultiplier;

        private void ComputeWaitTime()
        {
            // workerSpeed comes from m_workerThreadSpeed, which comes from trackBarSpeed, which ranges from 0 to 20

            //wait = (20 - workerSpeed) * 200; // The old formula, ranged from 4000 down to 0...
            //if (wait < 10)
            //    wait = 10; // ...well, down to 10 minimum

            if (m_WorkerThreadSpeed > 15)
            {
                m_WorkerWaitTime = 10;
                m_WorkMultiplier = m_WorkerThreadSpeed - 15; // 1 to 5 multiplier
            }
            else
            {
                int factor = 17 - m_WorkerThreadSpeed; // waits go up as speed goes down
                int wait = factor * factor * 10; // From 289*10 down to 1*10

                m_WorkerWaitTime = wait;
                m_WorkMultiplier = 1;
            }
        }

        public WorkerUI()
        {
            InitializeComponent();
            trackbarSpeed.Minimum = 0;
            trackbarSpeed.Maximum = 20;
            trackbarSpeed.Value = 10;
            m_WorkerThreadSpeed = trackbarSpeed.Value;
            ComputeWaitTime();
        }

        private void trackbarSpeed_ValueChanged(object sender, EventArgs e)
        {
            m_WorkerThreadSpeed = trackbarSpeed.Value;
            ComputeWaitTime();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Visible = true;
            btnStop.Visible = false;

            Stop();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        public void Start()
        {
            btnStart.Visible = false;
            btnStop.Visible = true;

            m_KeepRunning = true;
            // ReSharper disable UseObjectOrCollectionInitializer
            m_WorkerThread = new Thread(WorkerThread);
            // ReSharper restore UseObjectOrCollectionInitializer
            m_WorkerThread.IsBackground = true;
            m_WorkerThread.Start();
        }

        private void WorkerThread()
        {
            int progress1 = 0;
            int progress2 = 0;
            int progress3 = 0;

            while (m_KeepRunning)
            {
                int counter = 0;
                while (counter < m_WorkMultiplier)
                {
                    Trace.Write("Time to update the progress bars\n");
                    Trace.Write("and give the impression of some work being done");
                    Trace.WriteLine("!"); // Only a Trace.WriteLine() concludes the log message (or a Trace.Flush())
                    // Turning on Trace's autoflush will Flush() after each Trace.Write(),
                    // putting each Write() in a separate Gibraltar log message.

                    progress1++;

                    if (progress1 > 100)
                    {
                        // Demonstrate our ConsoleIntercepter
                        Console.Write("First bar has spilled over.\n"); // This should be on its own line and log message.
                        progress1 = 0;
                        progress2++;
                    }

                    if (progress2 > 100)
                    {
                        Console.Write("Second bar has spilled over");
                        progress2 = 0;
                        progress3++;
                        Console.WriteLine("!"); // This should be part of the previous line and log message.
                        // Warning: Console output is not thread-buffered and may have threading race conditions.
                    }

                    if (progress3 > 100)
                    {
                        Console.WriteLine("Third bar has spilled over!!!");
                        progress3 = 0;
                    }

                    verticalProgressBar1.ProgressInPercentage = progress1;
                    verticalProgressBar2.ProgressInPercentage = progress2;
                    verticalProgressBar3.ProgressInPercentage = progress3;

                    counter++;
                    if (counter < m_WorkMultiplier)
                    {
                        Thread.Sleep(0); // Just let others get some work done, but come right back to us
                    }
                    else
                    {
                        break;
                    }
                }

                // We always pause for at least a few milliseconds,
                // and depending on the slider, we may sleep a few seconds
                Thread.Sleep(m_WorkerWaitTime);
            }
        }

        public void Stop()
        {
            m_KeepRunning = false; // Stop worker thread
            if (m_WorkerThread != null)
                m_WorkerThread.Join(500);
        }

        private void WorkerUI_Resize(object sender, EventArgs e)
        {
            Width = 140;
        }

        private void btnException_Click(object sender, EventArgs e)
        {
            if (e != null) // This should always be true, but keeps the return below reachable.
                throw new Exception("Oops, let's pretend we never expected this to happen...");

            return; // Some place to set-next-statement in the debugger to get past the exception.
        }
    }
}
