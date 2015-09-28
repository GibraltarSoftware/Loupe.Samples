using System.Windows.Forms;

namespace Loupe.Extension.FindByUser
{
    partial class FindByUserConfigurationDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private GroupBox gbDatabase;
        private Label lblConnectionString;
        private Label lblProvider;
        private ComboBox cboProvider;
        private TextBox txtConnectionString;
        private CheckBox chkEnableAutoScan;


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
            this.gbDatabase = new System.Windows.Forms.GroupBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.lblProvider = new System.Windows.Forms.Label();
            this.cboProvider = new System.Windows.Forms.ComboBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.chkEnableAutoScan = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.gbDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDatabase
            // 
            this.gbDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatabase.Controls.Add(this.lblConnectionString);
            this.gbDatabase.Controls.Add(this.lblProvider);
            this.gbDatabase.Controls.Add(this.cboProvider);
            this.gbDatabase.Controls.Add(this.txtConnectionString);
            this.gbDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatabase.Location = new System.Drawing.Point(12, 7);
            this.gbDatabase.Name = "gbDatabase";
            this.gbDatabase.Size = new System.Drawing.Size(435, 147);
            this.gbDatabase.TabIndex = 0;
            this.gbDatabase.TabStop = false;
            this.gbDatabase.Text = "Database";
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionString.Location = new System.Drawing.Point(6, 65);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(94, 13);
            this.lblConnectionString.TabIndex = 2;
            this.lblConnectionString.Text = "Connection String:";
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvider.Location = new System.Drawing.Point(6, 27);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(98, 13);
            this.lblProvider.TabIndex = 2;
            this.lblProvider.Text = "Database Provider:";
            // 
            // cboProvider
            // 
            this.cboProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvider.FormattingEnabled = true;
            this.cboProvider.Items.AddRange(new object[] {
            "VistaDB",
            "SQL Server"});
            this.cboProvider.Location = new System.Drawing.Point(110, 21);
            this.cboProvider.Name = "cboProvider";
            this.cboProvider.Size = new System.Drawing.Size(169, 24);
            this.cboProvider.TabIndex = 1;
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionString.Location = new System.Drawing.Point(110, 60);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConnectionString.Size = new System.Drawing.Size(319, 72);
            this.txtConnectionString.TabIndex = 0;
            // 
            // chkEnableAutoScan
            // 
            this.chkEnableAutoScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEnableAutoScan.AutoSize = true;
            this.chkEnableAutoScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableAutoScan.Location = new System.Drawing.Point(12, 160);
            this.chkEnableAutoScan.Name = "chkEnableAutoScan";
            this.chkEnableAutoScan.Size = new System.Drawing.Size(158, 17);
            this.chkEnableAutoScan.TabIndex = 3;
            this.chkEnableAutoScan.Text = "Scan sessions automatically";
            this.chkEnableAutoScan.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(372, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(291, 205);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(12, 205);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(112, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset to Default";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FindByUserConfigurationDialog
            // 
            this.ClientSize = new System.Drawing.Size(459, 236);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbDatabase);
            this.Controls.Add(this.chkEnableAutoScan);
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "FindByUserConfigurationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find By User AddIn Configuration";
            this.gbDatabase.ResumeLayout(false);
            this.gbDatabase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Button btnCancel;
        private Button btnSave;
        private Button btnReset;
    }
}