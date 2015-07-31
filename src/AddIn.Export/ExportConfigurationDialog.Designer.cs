namespace Loupe.Extension.Export
{
    partial class ExportConfigurationDialog
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
            this.gbGeneral = new System.Windows.Forms.GroupBox();
            this.txtEnvironment = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkEnsureUniqueFilenames = new System.Windows.Forms.CheckBox();
            this.chkEnableAutoExport = new System.Windows.Forms.CheckBox();
            this.btnExportPathBrowse = new System.Windows.Forms.Button();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.lblExportPath = new System.Windows.Forms.Label();
            this.toolTipGenerator = new System.Windows.Forms.ToolTip(this.components);
            this.chkEnableLogMessageExport = new System.Windows.Forms.CheckBox();
            this.cboMinimumSeverity = new System.Windows.Forms.ComboBox();
            this.cboMessageFormatter = new System.Windows.Forms.ComboBox();
            this.chkIncludeSessionSummary = new System.Windows.Forms.CheckBox();
            this.chkIncludeExceptionDetails = new System.Windows.Forms.CheckBox();
            this.btnResetMetrics = new System.Windows.Forms.Button();
            this.btnDeleteComments = new System.Windows.Forms.Button();
            this.gbMetrics = new System.Windows.Forms.GroupBox();
            this.txtMetricsToExport = new System.Windows.Forms.TextBox();
            this.gbMessages = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.commandPanel.SuspendLayout();
            this.gbGeneral.SuspendLayout();
            this.gbMetrics.SuspendLayout();
            this.gbMessages.SuspendLayout();
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
            this.commandPanel.Location = new System.Drawing.Point(0, 524);
            this.commandPanel.Name = "commandPanel";
            this.commandPanel.RowCount = 1;
            this.commandPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.commandPanel.Size = new System.Drawing.Size(434, 37);
            this.commandPanel.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(220, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(140, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 27);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // gbGeneral
            // 
            this.gbGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGeneral.Controls.Add(this.txtEnvironment);
            this.gbGeneral.Controls.Add(this.label1);
            this.gbGeneral.Controls.Add(this.chkEnsureUniqueFilenames);
            this.gbGeneral.Controls.Add(this.chkEnableAutoExport);
            this.gbGeneral.Controls.Add(this.btnExportPathBrowse);
            this.gbGeneral.Controls.Add(this.txtExportPath);
            this.gbGeneral.Controls.Add(this.lblExportPath);
            this.gbGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGeneral.Location = new System.Drawing.Point(12, 12);
            this.gbGeneral.Name = "gbGeneral";
            this.gbGeneral.Size = new System.Drawing.Size(410, 119);
            this.gbGeneral.TabIndex = 9;
            this.gbGeneral.TabStop = false;
            this.gbGeneral.Text = "General Settings";
            // 
            // txtEnvironment
            // 
            this.txtEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnvironment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnvironment.Location = new System.Drawing.Point(103, 48);
            this.txtEnvironment.Name = "txtEnvironment";
            this.txtEnvironment.Size = new System.Drawing.Size(264, 20);
            this.txtEnvironment.TabIndex = 6;
            this.toolTipGenerator.SetToolTip(this.txtEnvironment, "Comma-delimited list of environments prmotion levels. Leave blank to not filter s" +
        "essions");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Environment:";
            // 
            // chkEnsureUniqueFilenames
            // 
            this.chkEnsureUniqueFilenames.AutoSize = true;
            this.chkEnsureUniqueFilenames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnsureUniqueFilenames.Location = new System.Drawing.Point(32, 97);
            this.chkEnsureUniqueFilenames.Name = "chkEnsureUniqueFilenames";
            this.chkEnsureUniqueFilenames.Size = new System.Drawing.Size(158, 17);
            this.chkEnsureUniqueFilenames.TabIndex = 4;
            this.chkEnsureUniqueFilenames.Text = "Ensure filename uniqueness";
            this.toolTipGenerator.SetToolTip(this.chkEnsureUniqueFilenames, "Include Session Id in filename because hostname and start time are not always uni" +
        "que");
            this.chkEnsureUniqueFilenames.UseVisualStyleBackColor = true;
            // 
            // chkEnableAutoExport
            // 
            this.chkEnableAutoExport.AutoSize = true;
            this.chkEnableAutoExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableAutoExport.Location = new System.Drawing.Point(32, 74);
            this.chkEnableAutoExport.Name = "chkEnableAutoExport";
            this.chkEnableAutoExport.Size = new System.Drawing.Size(163, 17);
            this.chkEnableAutoExport.TabIndex = 3;
            this.chkEnableAutoExport.Text = "Export sessions automatically";
            this.toolTipGenerator.SetToolTip(this.chkEnableAutoExport, "Export automatically as sessions are added");
            this.chkEnableAutoExport.UseVisualStyleBackColor = true;
            // 
            // btnExportPathBrowse
            // 
            this.btnExportPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportPathBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPathBrowse.Location = new System.Drawing.Point(373, 20);
            this.btnExportPathBrowse.Name = "btnExportPathBrowse";
            this.btnExportPathBrowse.Size = new System.Drawing.Size(31, 23);
            this.btnExportPathBrowse.TabIndex = 2;
            this.btnExportPathBrowse.Text = "...";
            this.btnExportPathBrowse.UseVisualStyleBackColor = true;
            this.btnExportPathBrowse.Click += new System.EventHandler(this.btnExportPathBrowse_Click);
            // 
            // txtExportPath
            // 
            this.txtExportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExportPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExportPath.Location = new System.Drawing.Point(103, 22);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(264, 20);
            this.txtExportPath.TabIndex = 1;
            this.toolTipGenerator.SetToolTip(this.txtExportPath, "The path to export sessions to");
            // 
            // lblExportPath
            // 
            this.lblExportPath.AutoSize = true;
            this.lblExportPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportPath.Location = new System.Drawing.Point(32, 25);
            this.lblExportPath.Name = "lblExportPath";
            this.lblExportPath.Size = new System.Drawing.Size(65, 13);
            this.lblExportPath.TabIndex = 0;
            this.lblExportPath.Text = "Export Path:";
            // 
            // chkEnableLogMessageExport
            // 
            this.chkEnableLogMessageExport.AutoSize = true;
            this.chkEnableLogMessageExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableLogMessageExport.Location = new System.Drawing.Point(9, 0);
            this.chkEnableLogMessageExport.Name = "chkEnableLogMessageExport";
            this.chkEnableLogMessageExport.Size = new System.Drawing.Size(158, 20);
            this.chkEnableLogMessageExport.TabIndex = 3;
            this.chkEnableLogMessageExport.Text = "Export Log Messages";
            this.toolTipGenerator.SetToolTip(this.chkEnableLogMessageExport, "Export log messages for each application in the Exported Applications and Metrics" +
        " list");
            this.chkEnableLogMessageExport.UseVisualStyleBackColor = true;
            // 
            // cboMinimumSeverity
            // 
            this.cboMinimumSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMinimumSeverity.FormattingEnabled = true;
            this.cboMinimumSeverity.Items.AddRange(new object[] {
            "Verbose",
            "Information",
            "Warning",
            "Error",
            "Critical"});
            this.cboMinimumSeverity.Location = new System.Drawing.Point(103, 52);
            this.cboMinimumSeverity.Name = "cboMinimumSeverity";
            this.cboMinimumSeverity.Size = new System.Drawing.Size(87, 21);
            this.cboMinimumSeverity.TabIndex = 4;
            this.toolTipGenerator.SetToolTip(this.cboMinimumSeverity, "Log messages below this severity won\'t be exported");
            // 
            // cboMessageFormatter
            // 
            this.cboMessageFormatter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMessageFormatter.FormattingEnabled = true;
            this.cboMessageFormatter.Items.AddRange(new object[] {
            "Default",
            "Summary",
            "Detailed"});
            this.cboMessageFormatter.Location = new System.Drawing.Point(103, 25);
            this.cboMessageFormatter.Name = "cboMessageFormatter";
            this.cboMessageFormatter.Size = new System.Drawing.Size(87, 21);
            this.cboMessageFormatter.TabIndex = 6;
            this.toolTipGenerator.SetToolTip(this.cboMessageFormatter, "Determines how much detail should be provided for each log message");
            // 
            // chkIncludeSessionSummary
            // 
            this.chkIncludeSessionSummary.AutoSize = true;
            this.chkIncludeSessionSummary.Location = new System.Drawing.Point(220, 27);
            this.chkIncludeSessionSummary.Name = "chkIncludeSessionSummary";
            this.chkIncludeSessionSummary.Size = new System.Drawing.Size(147, 17);
            this.chkIncludeSessionSummary.TabIndex = 3;
            this.chkIncludeSessionSummary.Text = "Include Session Summary";
            this.toolTipGenerator.SetToolTip(this.chkIncludeSessionSummary, "Export a session summary before listing individual log messages");
            this.chkIncludeSessionSummary.UseVisualStyleBackColor = true;
            // 
            // chkIncludeExceptionDetails
            // 
            this.chkIncludeExceptionDetails.AutoSize = true;
            this.chkIncludeExceptionDetails.Location = new System.Drawing.Point(220, 54);
            this.chkIncludeExceptionDetails.Name = "chkIncludeExceptionDetails";
            this.chkIncludeExceptionDetails.Size = new System.Drawing.Size(146, 17);
            this.chkIncludeExceptionDetails.TabIndex = 8;
            this.chkIncludeExceptionDetails.Text = "Include Exception Details";
            this.toolTipGenerator.SetToolTip(this.chkIncludeExceptionDetails, "Export exception details such as stack trace");
            this.chkIncludeExceptionDetails.UseVisualStyleBackColor = true;
            // 
            // btnResetMetrics
            // 
            this.btnResetMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetMetrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetMetrics.Location = new System.Drawing.Point(160, 255);
            this.btnResetMetrics.Name = "btnResetMetrics";
            this.btnResetMetrics.Size = new System.Drawing.Size(144, 23);
            this.btnResetMetrics.TabIndex = 6;
            this.btnResetMetrics.Text = "Reset Metric Configuration";
            this.toolTipGenerator.SetToolTip(this.btnResetMetrics, "Delete all metric config replacing with help comments");
            this.btnResetMetrics.UseVisualStyleBackColor = true;
            this.btnResetMetrics.Click += new System.EventHandler(this.btnResetMetrics_Click);
            // 
            // btnDeleteComments
            // 
            this.btnDeleteComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteComments.Location = new System.Drawing.Point(9, 255);
            this.btnDeleteComments.Name = "btnDeleteComments";
            this.btnDeleteComments.Size = new System.Drawing.Size(144, 23);
            this.btnDeleteComments.TabIndex = 5;
            this.btnDeleteComments.Text = "Delete All Comments";
            this.toolTipGenerator.SetToolTip(this.btnDeleteComments, "Deleting comments makes you finalized config more readable");
            this.btnDeleteComments.UseVisualStyleBackColor = true;
            this.btnDeleteComments.Click += new System.EventHandler(this.btnDeleteComments_Click);
            // 
            // gbMetrics
            // 
            this.gbMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMetrics.Controls.Add(this.btnResetMetrics);
            this.gbMetrics.Controls.Add(this.btnDeleteComments);
            this.gbMetrics.Controls.Add(this.txtMetricsToExport);
            this.gbMetrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMetrics.Location = new System.Drawing.Point(12, 146);
            this.gbMetrics.Name = "gbMetrics";
            this.gbMetrics.Size = new System.Drawing.Size(410, 286);
            this.gbMetrics.TabIndex = 10;
            this.gbMetrics.TabStop = false;
            this.gbMetrics.Text = "Exported Applications and Metrics";
            // 
            // txtMetricsToExport
            // 
            this.txtMetricsToExport.AcceptsReturn = true;
            this.txtMetricsToExport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMetricsToExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMetricsToExport.Location = new System.Drawing.Point(9, 23);
            this.txtMetricsToExport.Multiline = true;
            this.txtMetricsToExport.Name = "txtMetricsToExport";
            this.txtMetricsToExport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMetricsToExport.Size = new System.Drawing.Size(395, 227);
            this.txtMetricsToExport.TabIndex = 0;
            this.txtMetricsToExport.WordWrap = false;
            this.txtMetricsToExport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMetricsToExport_KeyDown);
            // 
            // gbMessages
            // 
            this.gbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMessages.Controls.Add(this.chkIncludeExceptionDetails);
            this.gbMessages.Controls.Add(this.label3);
            this.gbMessages.Controls.Add(this.chkIncludeSessionSummary);
            this.gbMessages.Controls.Add(this.cboMessageFormatter);
            this.gbMessages.Controls.Add(this.label2);
            this.gbMessages.Controls.Add(this.cboMinimumSeverity);
            this.gbMessages.Controls.Add(this.chkEnableLogMessageExport);
            this.gbMessages.Location = new System.Drawing.Point(12, 438);
            this.gbMessages.Name = "gbMessages";
            this.gbMessages.Size = new System.Drawing.Size(410, 80);
            this.gbMessages.TabIndex = 10;
            this.gbMessages.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Formatter:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Min Severity:";
            // 
            // ExportConfigurationDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(434, 561);
            this.Controls.Add(this.gbMessages);
            this.Controls.Add(this.gbMetrics);
            this.Controls.Add(this.gbGeneral);
            this.Controls.Add(this.commandPanel);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 500);
            this.Name = "ExportConfigurationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Session Export Configuration";
            this.commandPanel.ResumeLayout(false);
            this.gbGeneral.ResumeLayout(false);
            this.gbGeneral.PerformLayout();
            this.gbMetrics.ResumeLayout(false);
            this.gbMetrics.PerformLayout();
            this.gbMessages.ResumeLayout(false);
            this.gbMessages.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel commandPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbGeneral;
        private System.Windows.Forms.CheckBox chkEnableAutoExport;
        private System.Windows.Forms.Button btnExportPathBrowse;
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.Label lblExportPath;
        private System.Windows.Forms.ToolTip toolTipGenerator;
        private System.Windows.Forms.GroupBox gbMetrics;
        private System.Windows.Forms.TextBox txtMetricsToExport;
        private System.Windows.Forms.CheckBox chkEnsureUniqueFilenames;
        private System.Windows.Forms.GroupBox gbMessages;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboMessageFormatter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMinimumSeverity;
        private System.Windows.Forms.CheckBox chkEnableLogMessageExport;
        private System.Windows.Forms.CheckBox chkIncludeExceptionDetails;
        private System.Windows.Forms.CheckBox chkIncludeSessionSummary;
        private System.Windows.Forms.Button btnDeleteComments;
        private System.Windows.Forms.Button btnResetMetrics;
        private System.Windows.Forms.TextBox txtEnvironment;
        private System.Windows.Forms.Label label1;
    }
}