namespace Loupe.Extension.FogBugz.Internal
{
    partial class AddDefectDialog
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
            this.buttonLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.ProjectLabel = new System.Windows.Forms.Label();
            this.ProjectSelection = new System.Windows.Forms.ComboBox();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.DescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.CaseLabel = new System.Windows.Forms.Label();
            this.AreaLabel = new System.Windows.Forms.Label();
            this.AreaSelection = new System.Windows.Forms.ComboBox();
            this.chkUseFogBugzFonts = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLayoutPanel
            // 
            this.buttonLayoutPanel.ColumnCount = 4;
            this.buttonLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.buttonLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.buttonLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonLayoutPanel.Controls.Add(this.btnCancel, 2, 0);
            this.buttonLayoutPanel.Controls.Add(this.btnOk, 1, 0);
            this.buttonLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonLayoutPanel.Location = new System.Drawing.Point(0, 504);
            this.buttonLayoutPanel.Name = "buttonLayoutPanel";
            this.buttonLayoutPanel.RowCount = 1;
            this.buttonLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonLayoutPanel.Size = new System.Drawing.Size(619, 37);
            this.buttonLayoutPanel.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(312, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(232, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(74, 27);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // ProjectLabel
            // 
            this.ProjectLabel.AutoSize = true;
            this.ProjectLabel.Location = new System.Drawing.Point(12, 87);
            this.ProjectLabel.Name = "ProjectLabel";
            this.ProjectLabel.Size = new System.Drawing.Size(43, 13);
            this.ProjectLabel.TabIndex = 2;
            this.ProjectLabel.Text = "Project:";
            // 
            // ProjectSelection
            // 
            this.ProjectSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProjectSelection.FormattingEnabled = true;
            this.ProjectSelection.Location = new System.Drawing.Point(75, 84);
            this.ProjectSelection.Name = "ProjectSelection";
            this.ProjectSelection.Size = new System.Drawing.Size(231, 21);
            this.ProjectSelection.TabIndex = 3;
            this.ProjectSelection.SelectedValueChanged += new System.EventHandler(this.ProjectSelection_SelectedValueChanged);
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(75, 28);
            this.TitleTextBox.MaxLength = 128;
            this.TitleTextBox.Multiline = true;
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TitleTextBox.Size = new System.Drawing.Size(532, 50);
            this.TitleTextBox.TabIndex = 1;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(12, 31);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(30, 13);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Title:";
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Location = new System.Drawing.Point(12, 108);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(63, 13);
            this.DescriptionLabel.TabIndex = 7;
            this.DescriptionLabel.Text = "Description:";
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionTextBox.AutoWordSelection = true;
            this.DescriptionTextBox.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionTextBox.Location = new System.Drawing.Point(15, 127);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.DescriptionTextBox.Size = new System.Drawing.Size(594, 347);
            this.DescriptionTextBox.TabIndex = 8;
            this.DescriptionTextBox.Text = "";
            // 
            // CaseLabel
            // 
            this.CaseLabel.Location = new System.Drawing.Point(72, 9);
            this.CaseLabel.Name = "CaseLabel";
            this.CaseLabel.Size = new System.Drawing.Size(232, 16);
            this.CaseLabel.TabIndex = 6;
            this.CaseLabel.Text = "<Label>";
            this.CaseLabel.Click += new System.EventHandler(this.CaseLabel_Click);
            // 
            // AreaLabel
            // 
            this.AreaLabel.AutoSize = true;
            this.AreaLabel.Location = new System.Drawing.Point(338, 87);
            this.AreaLabel.Name = "AreaLabel";
            this.AreaLabel.Size = new System.Drawing.Size(32, 13);
            this.AreaLabel.TabIndex = 10;
            this.AreaLabel.Text = "Area:";
            // 
            // AreaSelection
            // 
            this.AreaSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AreaSelection.FormattingEnabled = true;
            this.AreaSelection.Location = new System.Drawing.Point(376, 84);
            this.AreaSelection.Name = "AreaSelection";
            this.AreaSelection.Size = new System.Drawing.Size(231, 21);
            this.AreaSelection.TabIndex = 11;
            this.AreaSelection.SelectedValueChanged += new System.EventHandler(this.AreaSelection_SelectedValueChanged);
            // 
            // chkUseFogBugzFonts
            // 
            this.chkUseFogBugzFonts.AutoSize = true;
            this.chkUseFogBugzFonts.Location = new System.Drawing.Point(15, 480);
            this.chkUseFogBugzFonts.Name = "chkUseFogBugzFonts";
            this.chkUseFogBugzFonts.Size = new System.Drawing.Size(116, 17);
            this.chkUseFogBugzFonts.TabIndex = 12;
            this.chkUseFogBugzFonts.Text = "Use FogBugz fonts";
            this.chkUseFogBugzFonts.UseVisualStyleBackColor = true;
            this.chkUseFogBugzFonts.CheckedChanged += new System.EventHandler(this.chkUseFogBugzFonts_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Case Id:";
            // 
            // AddDefectDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(619, 541);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkUseFogBugzFonts);
            this.Controls.Add(this.AreaLabel);
            this.Controls.Add(this.AreaSelection);
            this.Controls.Add(this.CaseLabel);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.DescriptionTextBox);
            this.Controls.Add(this.ProjectLabel);
            this.Controls.Add(this.ProjectSelection);
            this.Controls.Add(this.buttonLayoutPanel);
            this.Controls.Add(this.TitleTextBox);
            this.Controls.Add(this.TitleLabel);
            this.MinimumSize = new System.Drawing.Size(635, 577);
            this.Name = "AddDefectDialog";
            this.Text = "Create / Update FogBugz Case";
            this.buttonLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel buttonLayoutPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.ComboBox ProjectSelection;
        private System.Windows.Forms.Label ProjectLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.RichTextBox DescriptionTextBox;
        private System.Windows.Forms.Label CaseLabel;
        private System.Windows.Forms.Label AreaLabel;
        private System.Windows.Forms.ComboBox AreaSelection;
        private System.Windows.Forms.CheckBox chkUseFogBugzFonts;
        private System.Windows.Forms.Label label1;
    }
}