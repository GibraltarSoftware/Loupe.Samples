using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.Test
{
    public partial class AddDefectDialog : Form
    {
        public AddDefectDialog()
        {
            InitializeComponent();

            messagesGrid.AutoGenerateColumns = false;

            DataGridViewColumn newColumn = new DataGridViewColumn();
            newColumn.DataPropertyName = "Timestamp";
            newColumn.HeaderText = "Time";
            newColumn.Name = "Timestamp";
            newColumn.CellTemplate = new DataGridViewTextBoxCell();
            messagesGrid.Columns.Add(newColumn);

            newColumn = new DataGridViewColumn();
            newColumn.DataPropertyName = "Severity";
            newColumn.HeaderText = "Severity";
            newColumn.Name = "Severity";
            newColumn.CellTemplate = new DataGridViewTextBoxCell();
            newColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            messagesGrid.Columns.Add(newColumn);

            newColumn = new DataGridViewColumn();
            newColumn.DataPropertyName = "Caption";
            newColumn.HeaderText = "Caption";
            newColumn.Name = "Caption";
            newColumn.CellTemplate = new DataGridViewTextBoxCell();
            messagesGrid.Columns.Add(newColumn);

            newColumn = new DataGridViewColumn();
            newColumn.DataPropertyName = "Description";
            newColumn.HeaderText = "Description";
            newColumn.Name = "Description";
            newColumn.CellTemplate = new DataGridViewTextBoxCell();
            newColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            messagesGrid.Columns.Add(newColumn);

        }

        /// <summary>
        /// Process a user's Add Defect request
        /// </summary>
        /// <param name="messages">The set of messages to relate to the defect</param>
        /// <returns>Cancel or OK, indicating if the user added the defect or not.</returns>
        public DialogResult AddDefect(IList<ILogMessage> messages)
        {
            //Display the log messages they selected.
            messagesGrid.DataSource = new BindingList<ILogMessage>(messages);

            txtTitle.Text = messages[0].Caption;
            rtbDescription.Focus(); //this is what they should edit.

            return ShowDialog();
        }
    }
}
