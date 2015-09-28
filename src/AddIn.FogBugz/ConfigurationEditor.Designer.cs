namespace Loupe.Extension.FogBugz
{
    partial class ConfigurationEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationEditor));
            this.commandPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.mappingTab = new System.Windows.Forms.TabPage();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.btnRemoveRule = new System.Windows.Forms.Button();
            this.mappingsGrid = new System.Windows.Forms.DataGridView();
            this.ProductColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApplicationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VersionsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FogBugzProjectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FogBugzAreaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriorityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverTab = new System.Windows.Forms.TabPage();
            this.localAccountInformation = new System.Windows.Forms.GroupBox();
            this.btnLocalTest = new System.Windows.Forms.Button();
            this.txtLocalPassword = new System.Windows.Forms.TextBox();
            this.txtLocalUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.analysisTab = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.hubAccountInformation = new System.Windows.Forms.GroupBox();
            this.btnHubTest = new System.Windows.Forms.Button();
            this.txtHubPassword = new System.Windows.Forms.TextBox();
            this.txtHubUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAnalyzeOnHub = new System.Windows.Forms.CheckBox();
            this.chkEnableAutomaticAnalysis = new System.Windows.Forms.CheckBox();
            this.toolTipGenerator = new System.Windows.Forms.ToolTip(this.components);
            this.caseSelectionFilter = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LastUpdatedList = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CaseStatusList = new System.Windows.Forms.ComboBox();
            this.commandPanel.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.mappingTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mappingsGrid)).BeginInit();
            this.serverTab.SuspendLayout();
            this.localAccountInformation.SuspendLayout();
            this.panel2.SuspendLayout();
            this.analysisTab.SuspendLayout();
            this.panel1.SuspendLayout();
            this.hubAccountInformation.SuspendLayout();
            this.caseSelectionFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandPanel
            // 
            this.commandPanel.ColumnCount = 4;
            this.commandPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.commandPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.commandPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.commandPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.commandPanel.Controls.Add(this.btnCancel, 2, 0);
            this.commandPanel.Controls.Add(this.btnOK, 1, 0);
            this.commandPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.commandPanel.Location = new System.Drawing.Point(0, 350);
            this.commandPanel.Name = "commandPanel";
            this.commandPanel.RowCount = 1;
            this.commandPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.commandPanel.Size = new System.Drawing.Size(674, 40);
            this.commandPanel.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(260, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.mappingTab);
            this.mainTabControl.Controls.Add(this.serverTab);
            this.mainTabControl.Controls.Add(this.analysisTab);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(650, 332);
            this.mainTabControl.TabIndex = 6;
            // 
            // mappingTab
            // 
            this.mappingTab.Controls.Add(this.btnAddRule);
            this.mappingTab.Controls.Add(this.btnCopy);
            this.mappingTab.Controls.Add(this.lblInstruction);
            this.mappingTab.Controls.Add(this.btnRemoveRule);
            this.mappingTab.Controls.Add(this.mappingsGrid);
            this.mappingTab.Location = new System.Drawing.Point(4, 22);
            this.mappingTab.Name = "mappingTab";
            this.mappingTab.Padding = new System.Windows.Forms.Padding(3);
            this.mappingTab.Size = new System.Drawing.Size(642, 306);
            this.mappingTab.TabIndex = 0;
            this.mappingTab.Text = "Relationships";
            this.mappingTab.UseVisualStyleBackColor = true;
            // 
            // btnAddRule
            // 
            this.btnAddRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRule.Location = new System.Drawing.Point(396, 15);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(68, 28);
            this.btnAddRule.TabIndex = 21;
            this.btnAddRule.Text = "New";
            this.toolTipGenerator.SetToolTip(this.btnAddRule, "Create a new rule");
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(470, 15);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(68, 28);
            this.btnCopy.TabIndex = 22;
            this.btnCopy.Text = "Copy";
            this.toolTipGenerator.SetToolTip(this.btnCopy, "Copy the selected rule and edit it");
            this.btnCopy.UseVisualStyleBackColor = true;
            // 
            // lblInstruction
            // 
            this.lblInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInstruction.BackColor = System.Drawing.Color.Transparent;
            this.lblInstruction.Location = new System.Drawing.Point(12, 15);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(364, 28);
            this.lblInstruction.TabIndex = 19;
            this.lblInstruction.Text = "These settings map Loupe Sessions to FogBugz areas.\r\nThe rules below apply to" +
                " all users of your Hub.";
            // 
            // btnRemoveRule
            // 
            this.btnRemoveRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveRule.Location = new System.Drawing.Point(562, 15);
            this.btnRemoveRule.Name = "btnRemoveRule";
            this.btnRemoveRule.Size = new System.Drawing.Size(68, 28);
            this.btnRemoveRule.TabIndex = 20;
            this.btnRemoveRule.Text = "Remove";
            this.toolTipGenerator.SetToolTip(this.btnRemoveRule, "Remove the selected rule");
            this.btnRemoveRule.UseVisualStyleBackColor = true;
            this.btnRemoveRule.Click += new System.EventHandler(this.btnRemoveRule_Click);
            // 
            // mappingsGrid
            // 
            this.mappingsGrid.AllowUserToAddRows = false;
            this.mappingsGrid.AllowUserToDeleteRows = false;
            this.mappingsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mappingsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.mappingsGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.mappingsGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mappingsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.mappingsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.mappingsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductColumn,
            this.ApplicationColumn,
            this.VersionsColumn,
            this.FogBugzProjectColumn,
            this.FogBugzAreaColumn,
            this.PriorityColumn});
            this.mappingsGrid.Location = new System.Drawing.Point(12, 49);
            this.mappingsGrid.Margin = new System.Windows.Forms.Padding(12, 3, 12, 12);
            this.mappingsGrid.MultiSelect = false;
            this.mappingsGrid.Name = "mappingsGrid";
            this.mappingsGrid.ReadOnly = true;
            this.mappingsGrid.RowHeadersVisible = false;
            this.mappingsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mappingsGrid.Size = new System.Drawing.Size(618, 245);
            this.mappingsGrid.TabIndex = 0;
            this.mappingsGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mappingsGrid_CellDoubleClick);
            this.mappingsGrid.SelectionChanged += new System.EventHandler(this.mappingsGrid_SelectionChanged);
            // 
            // ProductColumn
            // 
            this.ProductColumn.DataPropertyName = "Product";
            this.ProductColumn.HeaderText = "Product";
            this.ProductColumn.Name = "ProductColumn";
            this.ProductColumn.ReadOnly = true;
            this.ProductColumn.Width = 69;
            // 
            // ApplicationColumn
            // 
            this.ApplicationColumn.DataPropertyName = "Application";
            this.ApplicationColumn.HeaderText = "Application";
            this.ApplicationColumn.Name = "ApplicationColumn";
            this.ApplicationColumn.ReadOnly = true;
            this.ApplicationColumn.Width = 84;
            // 
            // VersionsColumn
            // 
            this.VersionsColumn.DataPropertyName = "Versions";
            this.VersionsColumn.HeaderText = "Versions";
            this.VersionsColumn.Name = "VersionsColumn";
            this.VersionsColumn.ReadOnly = true;
            this.VersionsColumn.Width = 72;
            // 
            // FogBugzProjectColumn
            // 
            this.FogBugzProjectColumn.DataPropertyName = "Project";
            this.FogBugzProjectColumn.HeaderText = "FogBugz Project";
            this.FogBugzProjectColumn.Name = "FogBugzProjectColumn";
            this.FogBugzProjectColumn.ReadOnly = true;
            this.FogBugzProjectColumn.Width = 110;
            // 
            // FogBugzAreaColumn
            // 
            this.FogBugzAreaColumn.DataPropertyName = "Area";
            this.FogBugzAreaColumn.HeaderText = "FogBugz Area";
            this.FogBugzAreaColumn.Name = "FogBugzAreaColumn";
            this.FogBugzAreaColumn.ReadOnly = true;
            this.FogBugzAreaColumn.Width = 99;
            // 
            // PriorityColumn
            // 
            this.PriorityColumn.DataPropertyName = "Priority";
            this.PriorityColumn.HeaderText = "Priority";
            this.PriorityColumn.Name = "PriorityColumn";
            this.PriorityColumn.ReadOnly = true;
            this.PriorityColumn.Width = 63;
            // 
            // serverTab
            // 
            this.serverTab.Controls.Add(this.localAccountInformation);
            this.serverTab.Controls.Add(this.panel2);
            this.serverTab.Location = new System.Drawing.Point(4, 22);
            this.serverTab.Name = "serverTab";
            this.serverTab.Padding = new System.Windows.Forms.Padding(3);
            this.serverTab.Size = new System.Drawing.Size(642, 306);
            this.serverTab.TabIndex = 1;
            this.serverTab.Text = "FogBugz Connection";
            this.serverTab.UseVisualStyleBackColor = true;
            // 
            // localAccountInformation
            // 
            this.localAccountInformation.Controls.Add(this.btnLocalTest);
            this.localAccountInformation.Controls.Add(this.txtLocalPassword);
            this.localAccountInformation.Controls.Add(this.txtLocalUserName);
            this.localAccountInformation.Controls.Add(this.label2);
            this.localAccountInformation.Controls.Add(this.label1);
            this.localAccountInformation.Location = new System.Drawing.Point(12, 62);
            this.localAccountInformation.Name = "localAccountInformation";
            this.localAccountInformation.Size = new System.Drawing.Size(341, 102);
            this.localAccountInformation.TabIndex = 0;
            this.localAccountInformation.TabStop = false;
            this.localAccountInformation.Text = "Your Account";
            // 
            // btnLocalTest
            // 
            this.btnLocalTest.Location = new System.Drawing.Point(93, 69);
            this.btnLocalTest.Name = "btnLocalTest";
            this.btnLocalTest.Size = new System.Drawing.Size(74, 25);
            this.btnLocalTest.TabIndex = 4;
            this.btnLocalTest.Text = "Test";
            this.btnLocalTest.UseVisualStyleBackColor = true;
            this.btnLocalTest.Click += new System.EventHandler(this.btnLocalTest_Click);
            // 
            // txtLocalPassword
            // 
            this.txtLocalPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalPassword.Location = new System.Drawing.Point(93, 43);
            this.txtLocalPassword.Name = "txtLocalPassword";
            this.txtLocalPassword.Size = new System.Drawing.Size(233, 20);
            this.txtLocalPassword.TabIndex = 3;
            this.txtLocalPassword.UseSystemPasswordChar = true;
            this.txtLocalPassword.TextChanged += new System.EventHandler(this.txtLocalPassword_TextChanged);
            // 
            // txtLocalUserName
            // 
            this.txtLocalUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalUserName.Location = new System.Drawing.Point(93, 17);
            this.txtLocalUserName.Name = "txtLocalUserName";
            this.txtLocalUserName.Size = new System.Drawing.Size(233, 20);
            this.txtLocalUserName.TabIndex = 2;
            this.txtLocalUserName.TextChanged += new System.EventHandler(this.txtLocalUserName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.caseSelectionFilter);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtServerUrl);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Margin = new System.Windows.Forms.Padding(12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(615, 282);
            this.panel2.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(257, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Example:  http://FogBugz.yourcompany.com/api.asp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Server Url:";
            // 
            // txtServerUrl
            // 
            this.txtServerUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerUrl.Location = new System.Drawing.Point(63, 0);
            this.txtServerUrl.Name = "txtServerUrl";
            this.txtServerUrl.Size = new System.Drawing.Size(552, 20);
            this.txtServerUrl.TabIndex = 3;
            this.toolTipGenerator.SetToolTip(this.txtServerUrl, "The url to the FogBugz server\'s api, including protocol");
            this.txtServerUrl.TextChanged += new System.EventHandler(this.txtServerUrl_TextChanged);
            // 
            // analysisTab
            // 
            this.analysisTab.Controls.Add(this.panel1);
            this.analysisTab.Location = new System.Drawing.Point(4, 22);
            this.analysisTab.Name = "analysisTab";
            this.analysisTab.Size = new System.Drawing.Size(642, 306);
            this.analysisTab.TabIndex = 2;
            this.analysisTab.Text = "Automatic Case Management";
            this.analysisTab.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.hubAccountInformation);
            this.panel1.Controls.Add(this.chkAnalyzeOnHub);
            this.panel1.Controls.Add(this.chkEnableAutomaticAnalysis);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 282);
            this.panel1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(21, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(594, 125);
            this.label7.TabIndex = 6;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // hubAccountInformation
            // 
            this.hubAccountInformation.Controls.Add(this.btnHubTest);
            this.hubAccountInformation.Controls.Add(this.txtHubPassword);
            this.hubAccountInformation.Controls.Add(this.txtHubUserName);
            this.hubAccountInformation.Controls.Add(this.label4);
            this.hubAccountInformation.Controls.Add(this.label5);
            this.hubAccountInformation.Location = new System.Drawing.Point(0, 180);
            this.hubAccountInformation.Name = "hubAccountInformation";
            this.hubAccountInformation.Size = new System.Drawing.Size(343, 102);
            this.hubAccountInformation.TabIndex = 5;
            this.hubAccountInformation.TabStop = false;
            this.hubAccountInformation.Text = "Account to use on the Hub";
            // 
            // btnHubTest
            // 
            this.btnHubTest.Location = new System.Drawing.Point(93, 69);
            this.btnHubTest.Name = "btnHubTest";
            this.btnHubTest.Size = new System.Drawing.Size(74, 25);
            this.btnHubTest.TabIndex = 4;
            this.btnHubTest.Text = "Test";
            this.btnHubTest.UseVisualStyleBackColor = true;
            this.btnHubTest.Click += new System.EventHandler(this.btnHubTest_Click);
            // 
            // txtHubPassword
            // 
            this.txtHubPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHubPassword.Location = new System.Drawing.Point(93, 43);
            this.txtHubPassword.Name = "txtHubPassword";
            this.txtHubPassword.Size = new System.Drawing.Size(235, 20);
            this.txtHubPassword.TabIndex = 3;
            this.txtHubPassword.UseSystemPasswordChar = true;
            this.txtHubPassword.TextChanged += new System.EventHandler(this.txtHubPassword_TextChanged);
            // 
            // txtHubUserName
            // 
            this.txtHubUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHubUserName.Location = new System.Drawing.Point(93, 17);
            this.txtHubUserName.Name = "txtHubUserName";
            this.txtHubUserName.Size = new System.Drawing.Size(235, 20);
            this.txtHubUserName.TabIndex = 2;
            this.txtHubUserName.TextChanged += new System.EventHandler(this.txtHubUserName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "User Name:";
            // 
            // chkAnalyzeOnHub
            // 
            this.chkAnalyzeOnHub.AutoSize = true;
            this.chkAnalyzeOnHub.Location = new System.Drawing.Point(3, 157);
            this.chkAnalyzeOnHub.Name = "chkAnalyzeOnHub";
            this.chkAnalyzeOnHub.Size = new System.Drawing.Size(288, 17);
            this.chkAnalyzeOnHub.TabIndex = 1;
            this.chkAnalyzeOnHub.Text = "Perform automatic analysis on the Hub (Recommended)";
            this.chkAnalyzeOnHub.UseVisualStyleBackColor = true;
            this.chkAnalyzeOnHub.CheckedChanged += new System.EventHandler(this.chkAnalyzeOnHub_CheckedChanged);
            // 
            // chkEnableAutomaticAnalysis
            // 
            this.chkEnableAutomaticAnalysis.AutoSize = true;
            this.chkEnableAutomaticAnalysis.Location = new System.Drawing.Point(3, 3);
            this.chkEnableAutomaticAnalysis.Name = "chkEnableAutomaticAnalysis";
            this.chkEnableAutomaticAnalysis.Size = new System.Drawing.Size(173, 17);
            this.chkEnableAutomaticAnalysis.TabIndex = 0;
            this.chkEnableAutomaticAnalysis.Text = "Automatically Analyze Sessions";
            this.chkEnableAutomaticAnalysis.UseVisualStyleBackColor = true;
            this.chkEnableAutomaticAnalysis.CheckedChanged += new System.EventHandler(this.chkEnableAutomaticAnalysis_CheckedChanged);
            // 
            // caseSelectionFilter
            // 
            this.caseSelectionFilter.Controls.Add(this.CaseStatusList);
            this.caseSelectionFilter.Controls.Add(this.label10);
            this.caseSelectionFilter.Controls.Add(this.LastUpdatedList);
            this.caseSelectionFilter.Controls.Add(this.label9);
            this.caseSelectionFilter.Controls.Add(this.label8);
            this.caseSelectionFilter.Location = new System.Drawing.Point(0, 157);
            this.caseSelectionFilter.Name = "caseSelectionFilter";
            this.caseSelectionFilter.Size = new System.Drawing.Size(340, 122);
            this.caseSelectionFilter.TabIndex = 6;
            this.caseSelectionFilter.TabStop = false;
            this.caseSelectionFilter.Text = "Default Case Selection Criteria";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Status:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Last Updated Within:";
            // 
            // LastUpdatedList
            // 
            this.LastUpdatedList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LastUpdatedList.FormattingEnabled = true;
            this.LastUpdatedList.Location = new System.Drawing.Point(131, 87);
            this.LastUpdatedList.Name = "LastUpdatedList";
            this.LastUpdatedList.Size = new System.Drawing.Size(195, 21);
            this.LastUpdatedList.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(325, 32);
            this.label10.TabIndex = 3;
            this.label10.Text = "To maximize performance, you can restrict the range of cases that are checked for" +
                " Loupe sessions";
            // 
            // CaseStatusList
            // 
            this.CaseStatusList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CaseStatusList.FormattingEnabled = true;
            this.CaseStatusList.Location = new System.Drawing.Point(63, 51);
            this.CaseStatusList.Name = "CaseStatusList";
            this.CaseStatusList.Size = new System.Drawing.Size(147, 21);
            this.CaseStatusList.TabIndex = 4;
            // 
            // ConfigurationEditor
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(674, 390);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.commandPanel);
            this.MinimumSize = new System.Drawing.Size(416, 428);
            this.Name = "ConfigurationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FogBugz Integration Configuration";
            this.commandPanel.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.mappingTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mappingsGrid)).EndInit();
            this.serverTab.ResumeLayout(false);
            this.localAccountInformation.ResumeLayout(false);
            this.localAccountInformation.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.analysisTab.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.hubAccountInformation.ResumeLayout(false);
            this.hubAccountInformation.PerformLayout();
            this.caseSelectionFilter.ResumeLayout(false);
            this.caseSelectionFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel commandPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage mappingTab;
        private System.Windows.Forms.TabPage serverTab;
        private System.Windows.Forms.TextBox txtServerUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox localAccountInformation;
        private System.Windows.Forms.Button btnLocalTest;
        private System.Windows.Forms.TextBox txtLocalPassword;
        private System.Windows.Forms.TextBox txtLocalUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTipGenerator;
        private System.Windows.Forms.TabPage analysisTab;
        private System.Windows.Forms.CheckBox chkEnableAutomaticAnalysis;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAnalyzeOnHub;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView mappingsGrid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox hubAccountInformation;
        private System.Windows.Forms.Button btnHubTest;
        private System.Windows.Forms.TextBox txtHubPassword;
        private System.Windows.Forms.TextBox txtHubUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Button btnRemoveRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplicationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VersionsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FogBugzProjectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FogBugzAreaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriorityColumn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox caseSelectionFilter;
        private System.Windows.Forms.ComboBox LastUpdatedList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CaseStatusList;
    }
}