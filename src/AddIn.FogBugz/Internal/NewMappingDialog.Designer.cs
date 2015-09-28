namespace Loupe.Extension.FogBugz.Internal
{
    partial class NewMappingDialog
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
            this.mappingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ProductSelection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ApplicationSelection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVersions = new System.Windows.Forms.TextBox();
            this.toolTipGenerator = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.ProjectSelection = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.AreaSelection = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.PrioritySelection = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.commandPanel.SuspendLayout();
            this.mappingPanel.SuspendLayout();
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
            this.commandPanel.Location = new System.Drawing.Point(0, 228);
            this.commandPanel.Name = "commandPanel";
            this.commandPanel.RowCount = 1;
            this.commandPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.commandPanel.Size = new System.Drawing.Size(345, 37);
            this.commandPanel.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(175, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(95, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // mappingPanel
            // 
            this.mappingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mappingPanel.Controls.Add(this.PrioritySelection);
            this.mappingPanel.Controls.Add(this.label8);
            this.mappingPanel.Controls.Add(this.AreaSelection);
            this.mappingPanel.Controls.Add(this.label7);
            this.mappingPanel.Controls.Add(this.ProjectSelection);
            this.mappingPanel.Controls.Add(this.label6);
            this.mappingPanel.Controls.Add(this.label5);
            this.mappingPanel.Controls.Add(this.txtVersions);
            this.mappingPanel.Controls.Add(this.label4);
            this.mappingPanel.Controls.Add(this.ApplicationSelection);
            this.mappingPanel.Controls.Add(this.label3);
            this.mappingPanel.Controls.Add(this.ProductSelection);
            this.mappingPanel.Controls.Add(this.label2);
            this.mappingPanel.Controls.Add(this.label1);
            this.mappingPanel.Location = new System.Drawing.Point(12, 12);
            this.mappingPanel.Margin = new System.Windows.Forms.Padding(12);
            this.mappingPanel.Name = "mappingPanel";
            this.mappingPanel.Size = new System.Drawing.Size(322, 204);
            this.mappingPanel.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Map Loupe Sessions For:";
            // 
            // ProductSelection
            // 
            this.ProductSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductSelection.FormattingEnabled = true;
            this.ProductSelection.Location = new System.Drawing.Point(80, 16);
            this.ProductSelection.Name = "ProductSelection";
            this.ProductSelection.Size = new System.Drawing.Size(242, 21);
            this.ProductSelection.TabIndex = 2;
            this.ProductSelection.SelectedValueChanged += new System.EventHandler(this.ProductSelection_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Application:";
            // 
            // ApplicationSelection
            // 
            this.ApplicationSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplicationSelection.FormattingEnabled = true;
            this.ApplicationSelection.Location = new System.Drawing.Point(80, 43);
            this.ApplicationSelection.Name = "ApplicationSelection";
            this.ApplicationSelection.Size = new System.Drawing.Size(242, 21);
            this.ApplicationSelection.TabIndex = 4;
            this.ApplicationSelection.SelectedValueChanged += new System.EventHandler(this.ApplicationSelection_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Version(s):";
            // 
            // txtVersions
            // 
            this.txtVersions.Location = new System.Drawing.Point(80, 70);
            this.txtVersions.Name = "txtVersions";
            this.txtVersions.Size = new System.Drawing.Size(144, 20);
            this.txtVersions.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "To FogBugz As:";
            // 
            // ProjectSelection
            // 
            this.ProjectSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProjectSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProjectSelection.FormattingEnabled = true;
            this.ProjectSelection.Location = new System.Drawing.Point(80, 128);
            this.ProjectSelection.Name = "ProjectSelection";
            this.ProjectSelection.Size = new System.Drawing.Size(242, 21);
            this.ProjectSelection.TabIndex = 9;
            this.ProjectSelection.SelectedValueChanged += new System.EventHandler(this.ProjectSelection_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 131);
            this.label6.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Project:";
            // 
            // AreaSelection
            // 
            this.AreaSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AreaSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AreaSelection.FormattingEnabled = true;
            this.AreaSelection.Location = new System.Drawing.Point(80, 155);
            this.AreaSelection.Name = "AreaSelection";
            this.AreaSelection.Size = new System.Drawing.Size(242, 21);
            this.AreaSelection.TabIndex = 11;
            this.AreaSelection.SelectedValueChanged += new System.EventHandler(this.AreaSelection_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 158);
            this.label7.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Area:";
            // 
            // PrioritySelection
            // 
            this.PrioritySelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PrioritySelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrioritySelection.FormattingEnabled = true;
            this.PrioritySelection.Location = new System.Drawing.Point(80, 182);
            this.PrioritySelection.Name = "PrioritySelection";
            this.PrioritySelection.Size = new System.Drawing.Size(129, 21);
            this.PrioritySelection.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 185);
            this.label8.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Priority:";
            // 
            // NewMappingDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(345, 265);
            this.Controls.Add(this.mappingPanel);
            this.Controls.Add(this.commandPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewMappingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NewMappingDialog";
            this.commandPanel.ResumeLayout(false);
            this.mappingPanel.ResumeLayout(false);
            this.mappingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel commandPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel mappingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ApplicationSelection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ProductSelection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVersions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTipGenerator;
        private System.Windows.Forms.ComboBox PrioritySelection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox AreaSelection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ProjectSelection;
        private System.Windows.Forms.Label label6;
    }
}