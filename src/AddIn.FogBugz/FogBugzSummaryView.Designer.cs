namespace Loupe.Extension.FogBugz
{
    partial class FogBugzSummaryView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.caseListGridView = new System.Windows.Forms.DataGridView();
            this.CaseIdColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.TitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdatedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.caseListGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RefreshButton.Location = new System.Drawing.Point(0, 196);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(59, 24);
            this.RefreshButton.TabIndex = 16;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // caseListGridView
            // 
            this.caseListGridView.AllowUserToAddRows = false;
            this.caseListGridView.AllowUserToDeleteRows = false;
            this.caseListGridView.AllowUserToResizeRows = false;
            this.caseListGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.caseListGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.caseListGridView.BackgroundColor = System.Drawing.Color.White;
            this.caseListGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.caseListGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.caseListGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.caseListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.caseListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CaseIdColumn,
            this.TitleColumn,
            this.StatusColumn,
            this.UpdatedColumn});
            this.caseListGridView.GridColor = System.Drawing.Color.LightSteelBlue;
            this.caseListGridView.Location = new System.Drawing.Point(0, 0);
            this.caseListGridView.Margin = new System.Windows.Forms.Padding(0);
            this.caseListGridView.MultiSelect = false;
            this.caseListGridView.Name = "caseListGridView";
            this.caseListGridView.ReadOnly = true;
            this.caseListGridView.RowHeadersVisible = false;
            this.caseListGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.caseListGridView.Size = new System.Drawing.Size(356, 193);
            this.caseListGridView.TabIndex = 26;
            this.caseListGridView.SelectionChanged += new System.EventHandler(this.caseListGridView_SelectionChanged);
            this.caseListGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.caseListGridView_CellContentClick);
            // 
            // CaseIdColumn
            // 
            this.CaseIdColumn.DataPropertyName = "CaseId";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CaseIdColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.CaseIdColumn.FillWeight = 70F;
            this.CaseIdColumn.Frozen = true;
            this.CaseIdColumn.HeaderText = "Case";
            this.CaseIdColumn.Name = "CaseIdColumn";
            this.CaseIdColumn.ReadOnly = true;
            this.CaseIdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CaseIdColumn.Width = 50;
            // 
            // TitleColumn
            // 
            this.TitleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TitleColumn.DataPropertyName = "Title";
            this.TitleColumn.HeaderText = "Title";
            this.TitleColumn.MinimumWidth = 20;
            this.TitleColumn.Name = "TitleColumn";
            this.TitleColumn.ReadOnly = true;
            // 
            // StatusColumn
            // 
            this.StatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.StatusColumn.DataPropertyName = "Status";
            this.StatusColumn.FillWeight = 50F;
            this.StatusColumn.HeaderText = "Status";
            this.StatusColumn.Name = "StatusColumn";
            this.StatusColumn.ReadOnly = true;
            this.StatusColumn.Width = 62;
            // 
            // UpdatedColumn
            // 
            this.UpdatedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.UpdatedColumn.DataPropertyName = "LastUpdated";
            this.UpdatedColumn.FillWeight = 50F;
            this.UpdatedColumn.HeaderText = "Updated";
            this.UpdatedColumn.Name = "UpdatedColumn";
            this.UpdatedColumn.ReadOnly = true;
            this.UpdatedColumn.Width = 73;
            // 
            // FogBugzSummaryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.caseListGridView);
            this.Controls.Add(this.RefreshButton);
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "FogBugzSummaryView";
            this.Size = new System.Drawing.Size(356, 220);
            this.Enter += new System.EventHandler(this.FogBugzSummaryView_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.caseListGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.DataGridView caseListGridView;
        private System.Windows.Forms.DataGridViewLinkColumn CaseIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TitleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdatedColumn;
    }
}
