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
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Gibraltar.Agent;

#endregion

namespace Gibraltar.TraceMonitorSamples
{
    public partial class MainApp : Form
    {
        private List<WorkerUI> workerUIs = new List<WorkerUI>();

        public MainApp()
        {
            InitializeComponent();
            Trace.WriteLine("Gibraltar Sample Application starts now.", "Startup");
            AddWorker();
            Trace.WriteLine("Now we have a first worker, the show can start...", "Startup");
        }

        private void mnuAddWorker_Click(object sender, EventArgs e)
        {
            AddWorker();
        }

        private void AddWorker()
        {
            Trace.TraceInformation("Increasing work force by one...");

            // The first column is already there, but we don't have a Worker in that column yet, so don't add another column in this case
            if (workerUIs.Count != 0)
                tableLayoutPanel.ColumnCount++;

            tableLayoutPanel.SetColumnSpan(liveViewer, tableLayoutPanel.ColumnCount);

            WorkerUI workerUI = new WorkerUI();
            workerUIs.Add(workerUI);

            Trace.TraceInformation("We got a worker now, but no work space yet...");

            tableLayoutPanel.Controls.Add(workerUI, tableLayoutPanel.ColumnCount - 1, 0);

            workerUI.Dock = System.Windows.Forms.DockStyle.Fill;
            workerUI.Location = new System.Drawing.Point(3, 3);
            workerUI.Name = "workerUI1";
            workerUI.Size = new System.Drawing.Size(344, 158);
            workerUI.TabIndex = 1;

            Trace.TraceInformation("Okeydokey, our new worker just got a nice cubicle workspace of the size of {0} x {1}", workerUI.Size.Width, workerUI.Size.Height);

            if (workerUIs.Count == 1)
                Trace.TraceInformation("There is only one worker working, poor guy :-(");
            else
                Trace.TraceInformation("Another worker was added to keep the CPU busy. Now we have {0} workers working for us, wow!", workerUIs.Count);

            workerUI.Start();
        }

        private void MainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trace.TraceInformation("We're going down, so we better let the workers know...", "Shutdown");

            foreach (WorkerUI worker in workerUIs)
                worker.Stop();

            Trace.TraceInformation("Unemployment rate just raised, no workers anymore.", "Shutdown");
        }
    }
}