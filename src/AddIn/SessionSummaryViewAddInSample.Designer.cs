namespace Loupe.Extension.Sample
{
    partial class SessionSummaryViewAddInSample
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
            this.viewPanel = new System.Windows.Forms.Panel();
            this.searchProgressBar = new System.Windows.Forms.ProgressBar();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboColumnList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTipGenerator = new System.Windows.Forms.ToolTip(this.components);
            this.viewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.searchProgressBar);
            this.viewPanel.Controls.Add(this.btnSearch);
            this.viewPanel.Controls.Add(this.txtValue);
            this.viewPanel.Controls.Add(this.label2);
            this.viewPanel.Controls.Add(this.cboColumnList);
            this.viewPanel.Controls.Add(this.label1);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 0);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(262, 183);
            this.viewPanel.TabIndex = 0;
            // 
            // searchProgressBar
            // 
            this.searchProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchProgressBar.Location = new System.Drawing.Point(71, 89);
            this.searchProgressBar.Name = "searchProgressBar";
            this.searchProgressBar.Size = new System.Drawing.Size(127, 21);
            this.searchProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.searchProgressBar.TabIndex = 5;
            this.searchProgressBar.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(71, 60);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.toolTipGenerator.SetToolTip(this.btnSearch, "Search for values");
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(71, 34);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(188, 20);
            this.txtValue.TabIndex = 3;
            this.toolTipGenerator.SetToolTip(this.txtValue, "The value to search for.  Use * to match any value");
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search For:";
            // 
            // cboColumnList
            // 
            this.cboColumnList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboColumnList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumnList.FormattingEnabled = true;
            this.cboColumnList.Location = new System.Drawing.Point(71, 7);
            this.cboColumnList.Name = "cboColumnList";
            this.cboColumnList.Size = new System.Drawing.Size(188, 21);
            this.cboColumnList.TabIndex = 1;
            this.toolTipGenerator.SetToolTip(this.cboColumnList, "Select the value to search by");
            this.cboColumnList.SelectedValueChanged += new System.EventHandler(this.cboColumnList_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Within:";
            // 
            // SessionSummaryViewAddInSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewPanel);
            this.Name = "SessionSummaryViewAddInSample";
            this.Size = new System.Drawing.Size(262, 183);
            this.Enter += new System.EventHandler(this.SessionSummaryViewAddInSample_Enter);
            this.viewPanel.ResumeLayout(false);
            this.viewPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboColumnList;
        private System.Windows.Forms.ProgressBar searchProgressBar;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ToolTip toolTipGenerator;
    }
}
