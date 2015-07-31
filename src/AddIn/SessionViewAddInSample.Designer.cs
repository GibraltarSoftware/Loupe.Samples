namespace Loupe.Extension.Test
{
    partial class SessionViewAddInSample
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
            this.exceptionsGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.exceptionsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // exceptionsGrid
            // 
            this.exceptionsGrid.AllowUserToAddRows = false;
            this.exceptionsGrid.AllowUserToDeleteRows = false;
            this.exceptionsGrid.AllowUserToOrderColumns = true;
            this.exceptionsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.exceptionsGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.exceptionsGrid.BackgroundColor = System.Drawing.Color.White;
            this.exceptionsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.exceptionsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.exceptionsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exceptionsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.exceptionsGrid.Location = new System.Drawing.Point(0, 0);
            this.exceptionsGrid.Name = "exceptionsGrid";
            this.exceptionsGrid.ReadOnly = true;
            this.exceptionsGrid.RowHeadersWidth = 16;
            this.exceptionsGrid.Size = new System.Drawing.Size(538, 455);
            this.exceptionsGrid.TabIndex = 0;
            // 
            // SessionViewAddInSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.exceptionsGrid);
            this.Name = "SessionViewAddInSample";
            this.Size = new System.Drawing.Size(538, 455);
            ((System.ComponentModel.ISupportInitialize)(this.exceptionsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView exceptionsGrid;
    }
}
