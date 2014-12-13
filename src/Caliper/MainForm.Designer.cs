namespace CaliperTest
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDoWork = new System.Windows.Forms.Button();
            this.btnStartWorking = new System.Windows.Forms.Button();
            this.btnStopWorking = new System.Windows.Forms.Button();
            this.btnWorkRepeatedly = new System.Windows.Forms.Button();
            this.nudWorkRepeatedly = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkRepeatedly)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDoWork
            // 
            this.btnDoWork.Location = new System.Drawing.Point(13, 13);
            this.btnDoWork.Name = "btnDoWork";
            this.btnDoWork.Size = new System.Drawing.Size(105, 32);
            this.btnDoWork.TabIndex = 0;
            this.btnDoWork.Text = "Do Work";
            this.btnDoWork.UseVisualStyleBackColor = true;
            this.btnDoWork.Click += new System.EventHandler(this.btnDoWork_Click);
            // 
            // btnStartWorking
            // 
            this.btnStartWorking.Location = new System.Drawing.Point(13, 71);
            this.btnStartWorking.Name = "btnStartWorking";
            this.btnStartWorking.Size = new System.Drawing.Size(105, 32);
            this.btnStartWorking.TabIndex = 0;
            this.btnStartWorking.Text = "Start Working";
            this.btnStartWorking.UseVisualStyleBackColor = true;
            this.btnStartWorking.Click += new System.EventHandler(this.btnStartWorking_Click);
            // 
            // btnStopWorking
            // 
            this.btnStopWorking.Location = new System.Drawing.Point(13, 109);
            this.btnStopWorking.Name = "btnStopWorking";
            this.btnStopWorking.Size = new System.Drawing.Size(105, 32);
            this.btnStopWorking.TabIndex = 0;
            this.btnStopWorking.Text = "Stop Working";
            this.btnStopWorking.UseVisualStyleBackColor = true;
            this.btnStopWorking.Click += new System.EventHandler(this.btnStopWorking_Click);
            // 
            // btnWorkRepeatedly
            // 
            this.btnWorkRepeatedly.Location = new System.Drawing.Point(13, 167);
            this.btnWorkRepeatedly.Name = "btnWorkRepeatedly";
            this.btnWorkRepeatedly.Size = new System.Drawing.Size(105, 32);
            this.btnWorkRepeatedly.TabIndex = 0;
            this.btnWorkRepeatedly.Text = "Work Repeatedly";
            this.btnWorkRepeatedly.UseVisualStyleBackColor = true;
            this.btnWorkRepeatedly.Click += new System.EventHandler(this.btnWorkRepeatedly_Click);
            // 
            // nudWorkRepeatedly
            // 
            this.nudWorkRepeatedly.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWorkRepeatedly.Location = new System.Drawing.Point(13, 206);
            this.nudWorkRepeatedly.Maximum = new decimal(new int[] {
            1001,
            0,
            0,
            0});
            this.nudWorkRepeatedly.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWorkRepeatedly.Name = "nudWorkRepeatedly";
            this.nudWorkRepeatedly.Size = new System.Drawing.Size(105, 20);
            this.nudWorkRepeatedly.TabIndex = 1;
            this.nudWorkRepeatedly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudWorkRepeatedly.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(134, 250);
            this.Controls.Add(this.nudWorkRepeatedly);
            this.Controls.Add(this.btnWorkRepeatedly);
            this.Controls.Add(this.btnStopWorking);
            this.Controls.Add(this.btnStartWorking);
            this.Controls.Add(this.btnDoWork);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Caliper Tests";
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkRepeatedly)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDoWork;
        private System.Windows.Forms.Button btnStartWorking;
        private System.Windows.Forms.Button btnStopWorking;
        private System.Windows.Forms.Button btnWorkRepeatedly;
        private System.Windows.Forms.NumericUpDown nudWorkRepeatedly;
    }
}

