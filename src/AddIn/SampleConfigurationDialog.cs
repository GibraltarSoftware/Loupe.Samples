using System.Windows.Forms;
using Loupe.Extensibility.Client;

namespace Loupe.Extension.Test
{
    public partial class SampleConfigurationDialog : Form, IConfigurationEditor
    {
        private SampleAddInConfiguration m_Configuration;

        public SampleConfigurationDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called by Loupe to have the configuration editor display itself and edit the provided configuration
        /// </summary>
        /// <param name="context">The Repository Context provides a connection to the hosting environment.</param><param name="configuration">The current configuration.</param><param name="initialConfiguration">Indicates if the configuration has ever completed in the current environment.</param>
        /// <returns>
        /// DialogResult.OK if the configuration is complete and should be accepted as the new configuration.  Any other result to cancel.  If this
        ///             is the initial configuration and it is not OK the Extension will not be enabled.
        /// </returns>
        public DialogResult EditConfiguration(IRepositoryContext context, IRepositoryConfiguration configuration, bool initialConfiguration)
        {
            m_Configuration = configuration.Common as SampleAddInConfiguration ?? new SampleAddInConfiguration();

            DisplayConfiguration();

            DialogResult result = ShowDialog();
            if (result == DialogResult.OK)
            {
                //copy back our changes.
                m_Configuration.AutoExportSessions = chkEnableAutoExport.Checked;
                m_Configuration.SessionExportPath = txtSessionExportPath.Text;
                configuration.Common = m_Configuration;
            }

            return result;
        }

        private void DisplayConfiguration()
        {
            chkEnableAutoExport.Checked = m_Configuration.AutoExportSessions;
            txtSessionExportPath.Text = m_Configuration.SessionExportPath;
        }
    }
}
