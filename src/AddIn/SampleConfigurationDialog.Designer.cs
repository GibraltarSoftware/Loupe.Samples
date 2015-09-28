namespace Loupe.Extension.Sample
{
    partial class SampleConfigurationDialog
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
            this.components = new System.ComponentModel.Container();
            this.commandPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.sessionExportGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSessionExportPath = new System.Windows.Forms.TextBox();
            this.btnExportPathBrowse = new System.Windows.Forms.Button();
            this.chkEnableAutoExport = new System.Windows.Forms.CheckBox();
            this.toolTipGenerator = new System.Windows.Forms.ToolTip(this.components);
            this.commandPanel.SuspendLayout();
            this.sessionExportGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandPanel
            // 
            this.commandPanel.BackColor = System.Drawing.Color.Transparent;
            this.commandPanel.ColumnCount = 4;
            this.commandPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.commandPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.commandPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.commandPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.commandPanel.Controls.Add(this.btnCancel, 2, 0);
            this.commandPanel.Controls.Add(this.btnOK, 1, 0);
            this.commandPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.commandPanel.Location = new System.Drawing.Point(0, 100);
            this.commandPanel.Name = "commandPanel";
            this.commandPanel.RowCount = 1;
            this.commandPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.commandPanel.Size = new System.Drawing.Size(419, 37);
            this.commandPanel.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(212, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(132, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 27);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // sessionExportGroupBox
            // 
            this.sessionExportGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionExportGroupBox.Controls.Add(this.chkEnableAutoExport);
            this.sessionExportGroupBox.Controls.Add(this.btnExportPathBrowse);
            this.sessionExportGroupBox.Controls.Add(this.txtSessionExportPath);
            this.sessionExportGroupBox.Controls.Add(this.label1);
            this.sessionExportGroupBox.Location = new System.Drawing.Point(12, 12);
            this.sessionExportGroupBox.Name = "sessionExportGroupBox";
            this.sessionExportGroupBox.Size = new System.Drawing.Size(395, 72);
            this.sessionExportGroupBox.TabIndex = 9;
            this.sessionExportGroupBox.TabStop = false;
            this.sessionExportGroupBox.Text = "Automatic Session Export";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path:";
            // 
            // txtSessionExportPath
            // 
            this.txtSessionExportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSessionExportPath.Location = new System.Drawing.Point(44, 19);
            this.txtSessionExportPath.Name = "txtSessionExportPath";
            this.txtSessionExportPath.Size = new System.Drawing.Size(308, 20);
            this.txtSessionExportPath.TabIndex = 1;
            this.toolTipGenerator.SetToolTip(this.txtSessionExportPath, "The path to export sessions to");
            // 
            // btnExportPathBrowse
            // 
            this.btnExportPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportPathBrowse.Location = new System.Drawing.Point(358, 17);
            this.btnExportPathBrowse.Name = "btnExportPathBrowse";
            this.btnExportPathBrowse.Size = new System.Drawing.Size(31, 23);
            this.btnExportPathBrowse.TabIndex = 2;
            this.btnExportPathBrowse.Text = "...";
            this.btnExportPathBrowse.UseVisualStyleBackColor = true;
            // 
            // chkEnableAutoExport
            // 
            this.chkEnableAutoExport.AutoSize = true;
            this.chkEnableAutoExport.Location = new System.Drawing.Point(44, 45);
            this.chkEnableAutoExport.Name = "chkEnableAutoExport";
            this.chkEnableAutoExport.Size = new System.Drawing.Size(166, 17);
            this.chkEnableAutoExport.TabIndex = 3;
            this.chkEnableAutoExport.Text = "Export Sessions Automatically";
            this.toolTipGenerator.SetToolTip(this.chkEnableAutoExport, "When checked, session data will be exported automaticaly as new sessions are stor" +
                    "ed in the user repository");
            this.chkEnableAutoExport.UseVisualStyleBackColor = true;
            // 
            // SampleConfigurationDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(419, 137);
            this.Controls.Add(this.sessionExportGroupBox);
            this.Controls.Add(this.commandPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SampleConfigurationDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sample Add In Configuration";
            this.commandPanel.ResumeLayout(false);
            this.sessionExportGroupBox.ResumeLayout(false);
            this.sessionExportGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel commandPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox sessionExportGroupBox;
        private System.Windows.Forms.CheckBox chkEnableAutoExport;
        private System.Windows.Forms.Button btnExportPathBrowse;
        private System.Windows.Forms.TextBox txtSessionExportPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTipGenerator;
    }
}