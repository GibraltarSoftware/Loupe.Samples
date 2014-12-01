using System.Collections.Generic;

namespace Gibraltar.TraceMonitorSamples
{
    partial class MainApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainApp));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.ButtonArrow = new System.Windows.Forms.PictureBox();
            this.ButtonLabel = new System.Windows.Forms.Label();
            this.SliderArrow = new System.Windows.Forms.PictureBox();
            this.SliderLabel = new System.Windows.Forms.Label();
            this.StartThreadButton = new System.Windows.Forms.Button();
            this.Splitter = new System.Windows.Forms.SplitContainer();
            this.liveViewer = new Gibraltar.Agent.Windows.LiveLogViewer();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderArrow)).BeginInit();
            this.Splitter.Panel1.SuspendLayout();
            this.Splitter.Panel2.SuspendLayout();
            this.Splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.AutoScroll = true;
            this.tableLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Location = new System.Drawing.Point(134, 3);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(862, 201);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.ButtonArrow);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.ButtonLabel);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.SliderArrow);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.SliderLabel);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.StartThreadButton);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1001, 207);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1001, 232);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // ButtonArrow
            // 
            this.ButtonArrow.Image = global::Gibraltar.TraceMonitorSamples.Properties.Resources.Arrow_Left;
            this.ButtonArrow.Location = new System.Drawing.Point(85, 9);
            this.ButtonArrow.Name = "ButtonArrow";
            this.ButtonArrow.Size = new System.Drawing.Size(46, 38);
            this.ButtonArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ButtonArrow.TabIndex = 3;
            this.ButtonArrow.TabStop = false;
            // 
            // ButtonLabel
            // 
            this.ButtonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLabel.ForeColor = System.Drawing.Color.White;
            this.ButtonLabel.Location = new System.Drawing.Point(0, 1);
            this.ButtonLabel.Name = "ButtonLabel";
            this.ButtonLabel.Size = new System.Drawing.Size(79, 52);
            this.ButtonLabel.TabIndex = 2;
            this.ButtonLabel.Text = "Generate messages and exceptions";
            this.ButtonLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SliderArrow
            // 
            this.SliderArrow.Image = global::Gibraltar.TraceMonitorSamples.Properties.Resources.Arrow_Left;
            this.SliderArrow.Location = new System.Drawing.Point(85, 66);
            this.SliderArrow.Name = "SliderArrow";
            this.SliderArrow.Size = new System.Drawing.Size(46, 38);
            this.SliderArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SliderArrow.TabIndex = 4;
            this.SliderArrow.TabStop = false;
            // 
            // SliderLabel
            // 
            this.SliderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SliderLabel.ForeColor = System.Drawing.Color.White;
            this.SliderLabel.Location = new System.Drawing.Point(17, 57);
            this.SliderLabel.Name = "SliderLabel";
            this.SliderLabel.Size = new System.Drawing.Size(62, 52);
            this.SliderLabel.TabIndex = 2;
            this.SliderLabel.Text = "Adjust message rate";
            this.SliderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StartThreadButton
            // 
            this.StartThreadButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.StartThreadButton.Location = new System.Drawing.Point(6, 122);
            this.StartThreadButton.Name = "StartThreadButton";
            this.StartThreadButton.Size = new System.Drawing.Size(122, 57);
            this.StartThreadButton.TabIndex = 1;
            this.StartThreadButton.Text = "Start Another Thread";
            this.StartThreadButton.UseVisualStyleBackColor = false;
            this.StartThreadButton.Click += new System.EventHandler(this.mnuAddWorker_Click);
            // 
            // Splitter
            // 
            this.Splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter.Location = new System.Drawing.Point(0, 0);
            this.Splitter.Name = "Splitter";
            this.Splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Splitter.Panel1
            // 
            this.Splitter.Panel1.Controls.Add(this.toolStripContainer1);
            // 
            // Splitter.Panel2
            // 
            this.Splitter.Panel2.Controls.Add(this.liveViewer);
            this.Splitter.Size = new System.Drawing.Size(1001, 567);
            this.Splitter.SplitterDistance = 232;
            this.Splitter.TabIndex = 1;
            // 
            // liveViewer
            // 
            this.liveViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.liveViewer.Location = new System.Drawing.Point(0, 0);
            this.liveViewer.Name = "liveViewer";
            this.liveViewer.Size = new System.Drawing.Size(1001, 331);
            this.liveViewer.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1001, 567);
            this.Controls.Add(this.Splitter);
            this.Name = "MainApp";
            this.Text = "Gibraltar Trace Monitor Sample Application -- Shows LiveLogViewer on form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainApp_FormClosing);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderArrow)).EndInit();
            this.Splitter.Panel1.ResumeLayout(false);
            this.Splitter.Panel2.ResumeLayout(false);
            this.Splitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer Splitter;
        private Gibraltar.Agent.Windows.LiveLogViewer liveViewer;
        private System.Windows.Forms.Label SliderLabel;
        private System.Windows.Forms.Label ButtonLabel;
        private System.Windows.Forms.Button StartThreadButton;
        private System.Windows.Forms.PictureBox ButtonArrow;
        private System.Windows.Forms.PictureBox SliderArrow;
    }
}

