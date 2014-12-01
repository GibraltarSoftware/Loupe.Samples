using System;
using System.Windows.Forms;

namespace Gibraltar.AddIn.FogBugz.Internal
{
    /// <summary>
    /// This dialog prompts the user for filter criteria allowing us to
    /// quickly load a list of relevant FogBugz cases to inspect.
    /// </summary>
    internal partial class FogBugzFilter : Form
    {
        private RadioButton m_PreviousStatus;
        private RadioButton m_PreviousUpdateTime;

        public FogBugzFilter()
        {
            InitializeComponent();

            ActiveStatusRadioButton.Checked = true;
            QuarterRadioButton.Checked = true;

            m_PreviousStatus = GetSelectedStatusRadioButton();
            m_PreviousUpdateTime = GetSelectedUpdateTimeRadioButton();
        }

        /// <summary>
        /// A display caption for the filter
        /// </summary>
        public string Caption
        {
            get
            {
                RadioButton statusRadioButton = GetSelectedStatusRadioButton();
                RadioButton updateTimeRadioButton = GetSelectedUpdateTimeRadioButton();

                string captionString = string.Format("{0} cases", statusRadioButton.Text);
                if (HasUpdateTimeFilter)
                    captionString += string.Format(" updated within {0}", updateTimeRadioButton.Text);

                return captionString;
            }
        }

        /// <summary>
        /// The filter query string in FogBugz format
        /// </summary>
        public string Filter
        {
            get
            {
                RadioButton statusRadioButton = GetSelectedStatusRadioButton();
                RadioButton updateTimeRadioButton = GetSelectedUpdateTimeRadioButton();

                string queryString = string.Empty;
                if (HasStatusFilter)
                    queryString = string.Format("{0} ", statusRadioButton.Tag);

                if (HasUpdateTimeFilter)
                    queryString += updateTimeRadioButton.Tag;

                return queryString;
            }
        }

        /// <summary>
        /// Indicates if the current filter includes status
        /// </summary>
        public bool HasStatusFilter
        {
            get
            {
                return (GetSelectedStatusRadioButton() != AnyStatusRadioButton); 
            }
        }

        /// <summary>
        /// Indicates if the current filter includes update time
        /// </summary>
        public bool HasUpdateTimeFilter
        {
            get
            {
                return (GetSelectedUpdateTimeRadioButton() != AnytimeRadioButton);
            }
        }

        private RadioButton GetSelectedStatusRadioButton()
        {
            foreach (Control control in StatusGroupBox.Controls)
            {
                RadioButton radioButton = control as RadioButton;
                if (radioButton != null && radioButton.Checked)
                    return radioButton;
            }

            return AnyStatusRadioButton;
        }

        private RadioButton GetSelectedUpdateTimeRadioButton()
        {
            foreach (Control control in LastUpdatedGroupBox.Controls)
            {
                RadioButton radioButton = control as RadioButton;
                if (radioButton != null && radioButton.Checked)
                    return radioButton;
            }

            return AnytimeRadioButton;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            m_PreviousStatus = GetSelectedStatusRadioButton();
            m_PreviousUpdateTime = GetSelectedUpdateTimeRadioButton();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            m_PreviousStatus.Checked = true;
            m_PreviousUpdateTime.Checked = true;
        }
    }
}
