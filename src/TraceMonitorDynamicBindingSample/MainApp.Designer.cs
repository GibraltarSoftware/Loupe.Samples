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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ButtonArrow = new System.Windows.Forms.PictureBox();
            this.ButtonLabel = new System.Windows.Forms.Label();
            this.SliderArrow = new System.Windows.Forms.PictureBox();
            this.SliderLabel = new System.Windows.Forms.Label();
            this.StartThreadButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderArrow)).BeginInit();
            this.SuspendLayout();
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
            // ButtonArrow
            // 
            this.ButtonArrow.Image = global::Gibraltar.TraceMonitorSamples.Properties.Resources.Arrow_Left;
            this.ButtonArrow.Location = new System.Drawing.Point(87, 10);
            this.ButtonArrow.Name = "ButtonArrow";
            this.ButtonArrow.Size = new System.Drawing.Size(46, 38);
            this.ButtonArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ButtonArrow.TabIndex = 9;
            this.ButtonArrow.TabStop = false;
            // 
            // ButtonLabel
            // 
            this.ButtonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLabel.ForeColor = System.Drawing.Color.White;
            this.ButtonLabel.Location = new System.Drawing.Point(2, 2);
            this.ButtonLabel.Name = "ButtonLabel";
            this.ButtonLabel.Size = new System.Drawing.Size(79, 52);
            this.ButtonLabel.TabIndex = 8;
            this.ButtonLabel.Text = "Generate messages and exceptions";
            this.ButtonLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SliderArrow
            // 
            this.SliderArrow.Image = global::Gibraltar.TraceMonitorSamples.Properties.Resources.Arrow_Left;
            this.SliderArrow.Location = new System.Drawing.Point(87, 67);
            this.SliderArrow.Name = "SliderArrow";
            this.SliderArrow.Size = new System.Drawing.Size(46, 38);
            this.SliderArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SliderArrow.TabIndex = 10;
            this.SliderArrow.TabStop = false;
            // 
            // SliderLabel
            // 
            this.SliderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SliderLabel.ForeColor = System.Drawing.Color.White;
            this.SliderLabel.Location = new System.Drawing.Point(19, 58);
            this.SliderLabel.Name = "SliderLabel";
            this.SliderLabel.Size = new System.Drawing.Size(62, 52);
            this.SliderLabel.TabIndex = 7;
            this.SliderLabel.Text = "Adjust message rate";
            this.SliderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StartThreadButton
            // 
            this.StartThreadButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.StartThreadButton.Location = new System.Drawing.Point(8, 123);
            this.StartThreadButton.Name = "StartThreadButton";
            this.StartThreadButton.Size = new System.Drawing.Size(122, 57);
            this.StartThreadButton.TabIndex = 6;
            this.StartThreadButton.Text = "Start Another Thread";
            this.StartThreadButton.UseVisualStyleBackColor = false;
            this.StartThreadButton.Click += new System.EventHandler(this.StartThreadButton_Click);
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
            this.tableLayoutPanel.Location = new System.Drawing.Point(136, 4);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(714, 199);
            this.tableLayoutPanel.TabIndex = 5;
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(853, 205);
            this.Controls.Add(this.ButtonArrow);
            this.Controls.Add(this.ButtonLabel);
            this.Controls.Add(this.SliderArrow);
            this.Controls.Add(this.SliderLabel);
            this.Controls.Add(this.StartThreadButton);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MainApp";
            this.Text = "Gibraltar Trace Monitor Sample Application -- Press Ctrl-Alt-F5 to display Trace " +
                "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainApp_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ButtonArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderArrow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.PictureBox ButtonArrow;
        private System.Windows.Forms.Label ButtonLabel;
        private System.Windows.Forms.PictureBox SliderArrow;
        private System.Windows.Forms.Label SliderLabel;
        private System.Windows.Forms.Button StartThreadButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}

