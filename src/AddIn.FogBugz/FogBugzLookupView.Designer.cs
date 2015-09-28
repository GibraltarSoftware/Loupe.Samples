namespace Loupe.Extension.FogBugz
{
    partial class FogBugzLookupView
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.UpdatedLabel = new System.Windows.Forms.Label();
            this.CaseIdLabel = new System.Windows.Forms.Label();
            this.CaseDetailsPanel = new System.Windows.Forms.Panel();
            this.caseDetailsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ProjectLabel = new System.Windows.Forms.Label();
            this.LatestSummaryLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.CaseIdLinkLabel = new System.Windows.Forms.LinkLabel();
            this.CaseIdTextBox = new System.Windows.Forms.TextBox();
            this.CaseDetailsPanel.SuspendLayout();
            this.caseDetailsFlowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoEllipsis = true;
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(3, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(46, 13);
            this.TitleLabel.TabIndex = 10;
            this.TitleLabel.Text = "<Title>";
            // 
            // UpdatedLabel
            // 
            this.UpdatedLabel.AutoEllipsis = true;
            this.UpdatedLabel.AutoSize = true;
            this.UpdatedLabel.Location = new System.Drawing.Point(3, 16);
            this.UpdatedLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.UpdatedLabel.Name = "UpdatedLabel";
            this.UpdatedLabel.Size = new System.Drawing.Size(60, 13);
            this.UpdatedLabel.TabIndex = 14;
            this.UpdatedLabel.Text = "<Updated>";
            // 
            // CaseIdLabel
            // 
            this.CaseIdLabel.AutoSize = true;
            this.CaseIdLabel.Location = new System.Drawing.Point(4, 7);
            this.CaseIdLabel.Name = "CaseIdLabel";
            this.CaseIdLabel.Size = new System.Drawing.Size(46, 13);
            this.CaseIdLabel.TabIndex = 23;
            this.CaseIdLabel.Text = "Case Id:";
            // 
            // CaseDetailsPanel
            // 
            this.CaseDetailsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CaseDetailsPanel.BackColor = System.Drawing.Color.Transparent;
            this.CaseDetailsPanel.Controls.Add(this.caseDetailsFlowPanel);
            this.CaseDetailsPanel.Controls.Add(this.SearchButton);
            this.CaseDetailsPanel.Controls.Add(this.CaseIdLinkLabel);
            this.CaseDetailsPanel.Controls.Add(this.CaseIdTextBox);
            this.CaseDetailsPanel.Controls.Add(this.CaseIdLabel);
            this.CaseDetailsPanel.Location = new System.Drawing.Point(3, 3);
            this.CaseDetailsPanel.Name = "CaseDetailsPanel";
            this.CaseDetailsPanel.Size = new System.Drawing.Size(361, 202);
            this.CaseDetailsPanel.TabIndex = 25;
            // 
            // caseDetailsFlowPanel
            // 
            this.caseDetailsFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.caseDetailsFlowPanel.AutoScroll = true;
            this.caseDetailsFlowPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.caseDetailsFlowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.caseDetailsFlowPanel.Controls.Add(this.TitleLabel);
            this.caseDetailsFlowPanel.Controls.Add(this.UpdatedLabel);
            this.caseDetailsFlowPanel.Controls.Add(this.ProjectLabel);
            this.caseDetailsFlowPanel.Controls.Add(this.LatestSummaryLabel);
            this.caseDetailsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.caseDetailsFlowPanel.Location = new System.Drawing.Point(0, 31);
            this.caseDetailsFlowPanel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.caseDetailsFlowPanel.Name = "caseDetailsFlowPanel";
            this.caseDetailsFlowPanel.Size = new System.Drawing.Size(361, 171);
            this.caseDetailsFlowPanel.TabIndex = 30;
            this.caseDetailsFlowPanel.WrapContents = false;
            // 
            // ProjectLabel
            // 
            this.ProjectLabel.AutoEllipsis = true;
            this.ProjectLabel.AutoSize = true;
            this.ProjectLabel.Location = new System.Drawing.Point(3, 32);
            this.ProjectLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.ProjectLabel.Name = "ProjectLabel";
            this.ProjectLabel.Size = new System.Drawing.Size(52, 13);
            this.ProjectLabel.TabIndex = 28;
            this.ProjectLabel.Text = "<Project>";
            // 
            // LatestSummaryLabel
            // 
            this.LatestSummaryLabel.AutoEllipsis = true;
            this.LatestSummaryLabel.AutoSize = true;
            this.LatestSummaryLabel.Location = new System.Drawing.Point(3, 48);
            this.LatestSummaryLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.LatestSummaryLabel.Name = "LatestSummaryLabel";
            this.LatestSummaryLabel.Size = new System.Drawing.Size(91, 13);
            this.LatestSummaryLabel.TabIndex = 29;
            this.LatestSummaryLabel.Text = "<LatestSummary>";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(138, 2);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(31, 23);
            this.SearchButton.TabIndex = 27;
            this.SearchButton.Text = "Go";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // CaseIdLinkLabel
            // 
            this.CaseIdLinkLabel.AutoSize = true;
            this.CaseIdLinkLabel.Location = new System.Drawing.Point(175, 7);
            this.CaseIdLinkLabel.Name = "CaseIdLinkLabel";
            this.CaseIdLinkLabel.Size = new System.Drawing.Size(57, 13);
            this.CaseIdLinkLabel.TabIndex = 25;
            this.CaseIdLinkLabel.TabStop = true;
            this.CaseIdLinkLabel.Text = "View Case";
            this.CaseIdLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CaseIdLinkLabel_LinkClicked);
            // 
            // CaseIdTextBox
            // 
            this.CaseIdTextBox.AcceptsReturn = true;
            this.CaseIdTextBox.Location = new System.Drawing.Point(56, 4);
            this.CaseIdTextBox.Name = "CaseIdTextBox";
            this.CaseIdTextBox.Size = new System.Drawing.Size(76, 20);
            this.CaseIdTextBox.TabIndex = 24;
            this.CaseIdTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CaseIdTextBox_KeyDown);
            // 
            // FogBugzLookupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CaseDetailsPanel);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FogBugzLookupView";
            this.Size = new System.Drawing.Size(364, 208);
            this.Enter += new System.EventHandler(this.FogBugzSummaryView_Enter);
            this.CaseDetailsPanel.ResumeLayout(false);
            this.CaseDetailsPanel.PerformLayout();
            this.caseDetailsFlowPanel.ResumeLayout(false);
            this.caseDetailsFlowPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label UpdatedLabel;
        private System.Windows.Forms.Label CaseIdLabel;
        private System.Windows.Forms.Panel CaseDetailsPanel;
        private System.Windows.Forms.TextBox CaseIdTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.LinkLabel CaseIdLinkLabel;
        private System.Windows.Forms.Label ProjectLabel;
        private System.Windows.Forms.Label LatestSummaryLabel;
        private System.Windows.Forms.FlowLayoutPanel caseDetailsFlowPanel;
    }
}
