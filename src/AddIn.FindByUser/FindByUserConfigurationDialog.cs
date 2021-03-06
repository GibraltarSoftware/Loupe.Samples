﻿using System.Windows.Forms;
using Loupe.Extensibility.Client;

namespace Loupe.Extension.FindByUser
{
    public partial class FindByUserConfigurationDialog : Form, IConfigurationEditor
    {
        private IRepositoryContext _context;

        public FindByUserConfigurationDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called by Loupe to have the configuration editor display itself and edit the provided configuration
        /// </summary>
        public DialogResult EditConfiguration(IRepositoryContext context, IRepositoryConfiguration configuration, bool initialConfiguration)
        {
            _context = context;
            _context.Log.Verbose(ExtentionDefinition.LogCategory, "Begin editing Session Alert config", null);

            var newConfig = configuration.Common as FindByUserConfiguration ?? new FindByUserConfiguration();

            DisplayConfiguration(newConfig);

            DialogResult result = ShowDialog();
            if (result == DialogResult.OK)
            {
                //copy back our changes, but first make a clone to allow change logging
                var oldConfig = newConfig.Clone();

                newConfig.DatabaseProvider = cboProvider.Text;
                newConfig.ConnectionString = txtConnectionString.Text;
                newConfig.AutoScanSessions = chkEnableAutoScan.Checked;

                configuration.Common = newConfig; // this is redundant except in the crucial first-time initialization case!

                LogConfigurationChanges(newConfig, oldConfig);
            }
            else
            {
                _context.Log.Verbose(ExtentionDefinition.LogCategory, "Canceling FindByUser config dialog", null);
            }

            return result;
        }

        private void DisplayConfiguration(FindByUserConfiguration config)
        {
            AssignValueToCombobox(cboProvider, config.DatabaseProvider);
            txtConnectionString.Text = config.ConnectionString;
            chkEnableAutoScan.Checked = config.AutoScanSessions;
        }

        private void AssignValueToCombobox(ComboBox comboBox, string text)
        {
            if (comboBox.Items.Contains(text))
                comboBox.Text = text;
            else
            {
                comboBox.SelectedIndex = 0;
                _context.Log.Warning(ExtentionDefinition.LogCategory, "Cannot assign illegal value to combobox",
                    "\"{0}\" is not a legal value for {1}", text, comboBox.Name);
            }
        }

        private void LogConfigurationChanges(FindByUserConfiguration newConfig, FindByUserConfiguration oldConfig)
        {
            if (oldConfig.Equals(newConfig))
                _context.Log.Verbose(ExtentionDefinition.LogCategory, "No change to FindByUser config", null);
            else
            {
                _context.Log.Information(ExtentionDefinition.LogCategory, "FindByUser configuration changed",
                    string.Format("New Config: {0}\n\nOld Config: {1}", newConfig, oldConfig));
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            DisplayConfiguration(new FindByUserConfiguration());
        }
    }
}
