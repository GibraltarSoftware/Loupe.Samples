namespace Gibraltar.TraceMonitorSamples
{
    partial class WorkerUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trackbarSpeed = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.verticalProgressBar3 = new VerticalProgressBar();
            this.verticalProgressBar2 = new VerticalProgressBar();
            this.verticalProgressBar1 = new VerticalProgressBar();
            this.btnException = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackbarSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // trackbarSpeed
            // 
            this.trackbarSpeed.Location = new System.Drawing.Point(18, 72);
            this.trackbarSpeed.Maximum = 20;
            this.trackbarSpeed.Name = "trackbarSpeed";
            this.trackbarSpeed.Size = new System.Drawing.Size(104, 45);
            this.trackbarSpeed.TabIndex = 1;
            this.toolTip1.SetToolTip(this.trackbarSpeed, "Changes the speed of the workerthread");
            this.trackbarSpeed.ValueChanged += new System.EventHandler(this.trackbarSpeed_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Change Work Speed";
            // 
            // verticalProgressBar3
            // 
            this.verticalProgressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.verticalProgressBar3.BorderColor = System.Drawing.Color.Silver;
            this.verticalProgressBar3.BorderWidth = 1;
            this.verticalProgressBar3.FillColor = System.Drawing.Color.CornflowerBlue;
            this.verticalProgressBar3.Location = new System.Drawing.Point(99, 116);
            this.verticalProgressBar3.Name = "verticalProgressBar3";
            this.verticalProgressBar3.ProgressInPercentage = 40;
            this.verticalProgressBar3.Size = new System.Drawing.Size(23, 50);
            this.verticalProgressBar3.TabIndex = 5;
            this.verticalProgressBar3.Text = "verticalProgressBar3";
            // 
            // verticalProgressBar2
            // 
            this.verticalProgressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.verticalProgressBar2.BorderColor = System.Drawing.Color.Silver;
            this.verticalProgressBar2.BorderWidth = 1;
            this.verticalProgressBar2.FillColor = System.Drawing.Color.CornflowerBlue;
            this.verticalProgressBar2.Location = new System.Drawing.Point(59, 116);
            this.verticalProgressBar2.Name = "verticalProgressBar2";
            this.verticalProgressBar2.ProgressInPercentage = 40;
            this.verticalProgressBar2.Size = new System.Drawing.Size(23, 50);
            this.verticalProgressBar2.TabIndex = 4;
            this.verticalProgressBar2.Text = "verticalProgressBar2";
            // 
            // verticalProgressBar1
            // 
            this.verticalProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.verticalProgressBar1.BorderColor = System.Drawing.Color.Silver;
            this.verticalProgressBar1.BorderWidth = 1;
            this.verticalProgressBar1.FillColor = System.Drawing.Color.CornflowerBlue;
            this.verticalProgressBar1.Location = new System.Drawing.Point(18, 116);
            this.verticalProgressBar1.Name = "verticalProgressBar1";
            this.verticalProgressBar1.ProgressInPercentage = 40;
            this.verticalProgressBar1.Size = new System.Drawing.Size(23, 50);
            this.verticalProgressBar1.TabIndex = 2;
            this.verticalProgressBar1.Text = "verticalProgressBar1";
            // 
            // btnException
            // 
            this.btnException.Image = global::Gibraltar.TraceMonitorSamples.Properties.Resources.Exception;
            this.btnException.Location = new System.Drawing.Point(90, 11);
            this.btnException.Name = "btnException";
            this.btnException.Size = new System.Drawing.Size(32, 32);
            this.btnException.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnException, "Forces an unhandled exception.");
            this.btnException.UseVisualStyleBackColor = true;
            this.btnException.Click += new System.EventHandler(this.btnException_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::Gibraltar.TraceMonitorSamples.Properties.Resources.Stop;
            this.btnStop.Location = new System.Drawing.Point(18, 11);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(32, 32);
            this.btnStop.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnStop, "Stops the worker thread");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Image = global::Gibraltar.TraceMonitorSamples.Properties.Resources.Start;
            this.btnStart.Location = new System.Drawing.Point(18, 11);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(32, 32);
            this.btnStart.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnStart, "Start the worker thread");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // WorkerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnException);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.verticalProgressBar3);
            this.Controls.Add(this.verticalProgressBar2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.verticalProgressBar1);
            this.Controls.Add(this.trackbarSpeed);
            this.MinimumSize = new System.Drawing.Size(140, 183);
            this.Name = "WorkerUI";
            this.Size = new System.Drawing.Size(140, 183);
            this.Resize += new System.EventHandler(this.WorkerUI_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trackbarSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackbarSpeed;
        private System.Windows.Forms.Label label1;
        private VerticalProgressBar verticalProgressBar1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnException;
        private VerticalProgressBar verticalProgressBar2;
        private VerticalProgressBar verticalProgressBar3;
    }
}
