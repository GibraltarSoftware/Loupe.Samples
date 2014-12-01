namespace Gibraltar.AddIn.FogBugz.Internal
{
    partial class FogBugzFilter
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.LastUpdatedGroupBox = new System.Windows.Forms.GroupBox();
            this.DayRadioButton = new System.Windows.Forms.RadioButton();
            this.WeekRadioButton = new System.Windows.Forms.RadioButton();
            this.MonthRadioButton = new System.Windows.Forms.RadioButton();
            this.QuarterRadioButton = new System.Windows.Forms.RadioButton();
            this.YearRadioButton = new System.Windows.Forms.RadioButton();
            this.AnytimeRadioButton = new System.Windows.Forms.RadioButton();
            this.StatusGroupBox = new System.Windows.Forms.GroupBox();
            this.ClosedStatusRadioButton = new System.Windows.Forms.RadioButton();
            this.ResolvedStatusRadioButton = new System.Windows.Forms.RadioButton();
            this.ActiveStatusRadioButton = new System.Windows.Forms.RadioButton();
            this.AnyStatusRadioButton = new System.Windows.Forms.RadioButton();
            this.LastUpdatedGroupBox.SuspendLayout();
            this.StatusGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(308, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(308, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // LastUpdatedGroupBox
            // 
            this.LastUpdatedGroupBox.Controls.Add(this.DayRadioButton);
            this.LastUpdatedGroupBox.Controls.Add(this.WeekRadioButton);
            this.LastUpdatedGroupBox.Controls.Add(this.MonthRadioButton);
            this.LastUpdatedGroupBox.Controls.Add(this.QuarterRadioButton);
            this.LastUpdatedGroupBox.Controls.Add(this.YearRadioButton);
            this.LastUpdatedGroupBox.Controls.Add(this.AnytimeRadioButton);
            this.LastUpdatedGroupBox.Location = new System.Drawing.Point(152, 12);
            this.LastUpdatedGroupBox.Name = "LastUpdatedGroupBox";
            this.LastUpdatedGroupBox.Size = new System.Drawing.Size(142, 162);
            this.LastUpdatedGroupBox.TabIndex = 1;
            this.LastUpdatedGroupBox.TabStop = false;
            this.LastUpdatedGroupBox.Text = "Last Updated Within";
            // 
            // DayRadioButton
            // 
            this.DayRadioButton.AutoSize = true;
            this.DayRadioButton.Location = new System.Drawing.Point(18, 134);
            this.DayRadioButton.Name = "DayRadioButton";
            this.DayRadioButton.Size = new System.Drawing.Size(53, 17);
            this.DayRadioButton.TabIndex = 6;
            this.DayRadioButton.Tag = "edited:\"yesterday..\"";
            this.DayRadioButton.Text = "1 Day";
            this.DayRadioButton.UseVisualStyleBackColor = true;
            // 
            // WeekRadioButton
            // 
            this.WeekRadioButton.AutoSize = true;
            this.WeekRadioButton.Location = new System.Drawing.Point(18, 111);
            this.WeekRadioButton.Name = "WeekRadioButton";
            this.WeekRadioButton.Size = new System.Drawing.Size(63, 17);
            this.WeekRadioButton.TabIndex = 6;
            this.WeekRadioButton.Tag = "edited:\"-7d..\"";
            this.WeekRadioButton.Text = "1 Week";
            this.WeekRadioButton.UseVisualStyleBackColor = true;
            // 
            // MonthRadioButton
            // 
            this.MonthRadioButton.AutoSize = true;
            this.MonthRadioButton.Location = new System.Drawing.Point(18, 88);
            this.MonthRadioButton.Name = "MonthRadioButton";
            this.MonthRadioButton.Size = new System.Drawing.Size(64, 17);
            this.MonthRadioButton.TabIndex = 6;
            this.MonthRadioButton.Tag = "edited:\"-31d..\"";
            this.MonthRadioButton.Text = "1 Month";
            this.MonthRadioButton.UseVisualStyleBackColor = true;
            // 
            // QuarterRadioButton
            // 
            this.QuarterRadioButton.AutoSize = true;
            this.QuarterRadioButton.Location = new System.Drawing.Point(18, 65);
            this.QuarterRadioButton.Name = "QuarterRadioButton";
            this.QuarterRadioButton.Size = new System.Drawing.Size(69, 17);
            this.QuarterRadioButton.TabIndex = 5;
            this.QuarterRadioButton.Tag = "edited:\"-93d..\"";
            this.QuarterRadioButton.Text = "3 Months";
            this.QuarterRadioButton.UseVisualStyleBackColor = true;
            // 
            // YearRadioButton
            // 
            this.YearRadioButton.AutoSize = true;
            this.YearRadioButton.Location = new System.Drawing.Point(18, 42);
            this.YearRadioButton.Name = "YearRadioButton";
            this.YearRadioButton.Size = new System.Drawing.Size(56, 17);
            this.YearRadioButton.TabIndex = 4;
            this.YearRadioButton.Tag = "edited:\"-365d..\"";
            this.YearRadioButton.Text = "1 Year";
            this.YearRadioButton.UseVisualStyleBackColor = true;
            // 
            // AnytimeRadioButton
            // 
            this.AnytimeRadioButton.AutoSize = true;
            this.AnytimeRadioButton.Location = new System.Drawing.Point(18, 19);
            this.AnytimeRadioButton.Name = "AnytimeRadioButton";
            this.AnytimeRadioButton.Size = new System.Drawing.Size(60, 17);
            this.AnytimeRadioButton.TabIndex = 3;
            this.AnytimeRadioButton.Tag = "";
            this.AnytimeRadioButton.Text = "Eternity";
            this.AnytimeRadioButton.UseVisualStyleBackColor = true;
            // 
            // StatusGroupBox
            // 
            this.StatusGroupBox.Controls.Add(this.ClosedStatusRadioButton);
            this.StatusGroupBox.Controls.Add(this.ResolvedStatusRadioButton);
            this.StatusGroupBox.Controls.Add(this.ActiveStatusRadioButton);
            this.StatusGroupBox.Controls.Add(this.AnyStatusRadioButton);
            this.StatusGroupBox.Location = new System.Drawing.Point(12, 12);
            this.StatusGroupBox.Name = "StatusGroupBox";
            this.StatusGroupBox.Size = new System.Drawing.Size(134, 162);
            this.StatusGroupBox.TabIndex = 0;
            this.StatusGroupBox.TabStop = false;
            this.StatusGroupBox.Text = "Case Status";
            // 
            // ClosedStatusRadioButton
            // 
            this.ClosedStatusRadioButton.AutoSize = true;
            this.ClosedStatusRadioButton.Location = new System.Drawing.Point(18, 88);
            this.ClosedStatusRadioButton.Name = "ClosedStatusRadioButton";
            this.ClosedStatusRadioButton.Size = new System.Drawing.Size(57, 17);
            this.ClosedStatusRadioButton.TabIndex = 2;
            this.ClosedStatusRadioButton.Tag = "status:Closed";
            this.ClosedStatusRadioButton.Text = "Closed";
            this.ClosedStatusRadioButton.UseVisualStyleBackColor = true;
            // 
            // ResolvedStatusRadioButton
            // 
            this.ResolvedStatusRadioButton.AutoSize = true;
            this.ResolvedStatusRadioButton.Location = new System.Drawing.Point(18, 65);
            this.ResolvedStatusRadioButton.Name = "ResolvedStatusRadioButton";
            this.ResolvedStatusRadioButton.Size = new System.Drawing.Size(70, 17);
            this.ResolvedStatusRadioButton.TabIndex = 1;
            this.ResolvedStatusRadioButton.Tag = "status:Resolved";
            this.ResolvedStatusRadioButton.Text = "Resolved";
            this.ResolvedStatusRadioButton.UseVisualStyleBackColor = true;
            // 
            // ActiveStatusRadioButton
            // 
            this.ActiveStatusRadioButton.AutoSize = true;
            this.ActiveStatusRadioButton.Location = new System.Drawing.Point(18, 42);
            this.ActiveStatusRadioButton.Name = "ActiveStatusRadioButton";
            this.ActiveStatusRadioButton.Size = new System.Drawing.Size(55, 17);
            this.ActiveStatusRadioButton.TabIndex = 1;
            this.ActiveStatusRadioButton.Tag = "status:Active";
            this.ActiveStatusRadioButton.Text = "Active";
            this.ActiveStatusRadioButton.UseVisualStyleBackColor = true;
            // 
            // AnyStatusRadioButton
            // 
            this.AnyStatusRadioButton.AutoSize = true;
            this.AnyStatusRadioButton.Location = new System.Drawing.Point(18, 19);
            this.AnyStatusRadioButton.Name = "AnyStatusRadioButton";
            this.AnyStatusRadioButton.Size = new System.Drawing.Size(59, 17);
            this.AnyStatusRadioButton.TabIndex = 0;
            this.AnyStatusRadioButton.Text = "Any/All";
            this.AnyStatusRadioButton.UseVisualStyleBackColor = true;
            // 
            // FogBugzFilter
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(395, 184);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.LastUpdatedGroupBox);
            this.Controls.Add(this.StatusGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FogBugzFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Case Selection Criteria";
            this.LastUpdatedGroupBox.ResumeLayout(false);
            this.LastUpdatedGroupBox.PerformLayout();
            this.StatusGroupBox.ResumeLayout(false);
            this.StatusGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox LastUpdatedGroupBox;
        private System.Windows.Forms.RadioButton DayRadioButton;
        private System.Windows.Forms.RadioButton WeekRadioButton;
        private System.Windows.Forms.RadioButton MonthRadioButton;
        private System.Windows.Forms.RadioButton QuarterRadioButton;
        private System.Windows.Forms.RadioButton YearRadioButton;
        private System.Windows.Forms.RadioButton AnytimeRadioButton;
        private System.Windows.Forms.GroupBox StatusGroupBox;
        private System.Windows.Forms.RadioButton ClosedStatusRadioButton;
        private System.Windows.Forms.RadioButton ResolvedStatusRadioButton;
        private System.Windows.Forms.RadioButton ActiveStatusRadioButton;
        private System.Windows.Forms.RadioButton AnyStatusRadioButton;
    }
}