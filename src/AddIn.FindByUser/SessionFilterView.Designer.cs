namespace Gibraltar.AddIn.FindByUser
{
    sealed partial class SessionFilterView
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
            this.lblUser = new System.Windows.Forms.Label();
            this.cboUser = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.calSessionDate = new System.Windows.Forms.MonthCalendar();
            this.pnlBadConfig = new System.Windows.Forms.Panel();
            this.btnEditConfig = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlBadConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(16, 14);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(32, 13);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "User:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cboUser
            // 
            this.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUser.FormattingEnabled = true;
            this.cboUser.Location = new System.Drawing.Point(54, 11);
            this.cboUser.Name = "cboUser";
            this.cboUser.Size = new System.Drawing.Size(227, 21);
            this.cboUser.TabIndex = 2;
            this.cboUser.SelectedIndexChanged += new System.EventHandler(this.cboUser_SelectedIndexChanged);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(16, 50);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 13);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Date:";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(287, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(64, 21);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // calSessionDate
            // 
            this.calSessionDate.Location = new System.Drawing.Point(54, 37);
            this.calSessionDate.MaxSelectionCount = 1;
            this.calSessionDate.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.calSessionDate.Name = "calSessionDate";
            this.calSessionDate.ShowTodayCircle = false;
            this.calSessionDate.TabIndex = 4;
            this.calSessionDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calSessionDate_DateChanged);
            // 
            // pnlBadConfig
            // 
            this.pnlBadConfig.Controls.Add(this.lblMessage);
            this.pnlBadConfig.Controls.Add(this.btnEditConfig);
            this.pnlBadConfig.Location = new System.Drawing.Point(3, 66);
            this.pnlBadConfig.Name = "pnlBadConfig";
            this.pnlBadConfig.Size = new System.Drawing.Size(336, 133);
            this.pnlBadConfig.TabIndex = 5;
            this.pnlBadConfig.Visible = false;
            // 
            // btnEditConfig
            // 
            this.btnEditConfig.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnEditConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditConfig.Location = new System.Drawing.Point(70, 75);
            this.btnEditConfig.Name = "btnEditConfig";
            this.btnEditConfig.Size = new System.Drawing.Size(208, 41);
            this.btnEditConfig.TabIndex = 0;
            this.btnEditConfig.Text = "Edit Configuration";
            this.btnEditConfig.UseVisualStyleBackColor = true;
            this.btnEditConfig.Click += new System.EventHandler(this.btnEditConfig_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblMessage.Location = new System.Drawing.Point(33, 15);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(278, 57);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "There is a problem accessing the database associated with the FindByUser add-in. " +
    " Please check your database connection string.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SessionFilterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBadConfig);
            this.Controls.Add(this.calSessionDate);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cboUser);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblUser);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "SessionFilterView";
            this.Size = new System.Drawing.Size(364, 208);
            this.Enter += new System.EventHandler(this.UserLookupView_Enter);
            this.pnlBadConfig.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.MonthCalendar calSessionDate;
        private System.Windows.Forms.Panel pnlBadConfig;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnEditConfig;
    }
}
