using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Loupe.Extensibility.Client;
using Loupe.Extensibility.Data;

namespace Loupe.Extension.Test
{
    public partial class SessionViewAddInSample : UserControl, ISessionView
    {
        private ISession m_Session;
        private string m_Title;

        /// <summary>
        /// Raised every time the title text for the view changes.
        /// </summary>
        public event EventHandler TitleChanged;

        public SessionViewAddInSample()
        {
            InitializeComponent();

            exceptionsGrid.AutoGenerateColumns = false;

            DataGridViewColumn newColumn = new DataGridViewColumn();
            newColumn.DataPropertyName = "TypeName";
            newColumn.HeaderText = "Type";
            newColumn.Name = "TypeName";
            newColumn.CellTemplate = new DataGridViewTextBoxCell(); 
            exceptionsGrid.Columns.Add(newColumn);

            newColumn = new DataGridViewColumn();
            newColumn.DataPropertyName = "Message";
            newColumn.HeaderText = "Message";
            newColumn.Name = "Message";
            newColumn.CellTemplate = new DataGridViewTextBoxCell();
            newColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            exceptionsGrid.Columns.Add(newColumn);

            newColumn = new DataGridViewColumn();
            newColumn.DataPropertyName = "Source";
            newColumn.HeaderText = "Source";
            newColumn.Name = "Source";
            newColumn.CellTemplate = new DataGridViewTextBoxCell();
            exceptionsGrid.Columns.Add(newColumn);

            newColumn = new DataGridViewColumn();
            newColumn.DataPropertyName = "StackTrace";
            newColumn.HeaderText = "StackTrace";
            newColumn.Name = "StackTrace";
            newColumn.CellTemplate = new DataGridViewTextBoxCell();
            newColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            exceptionsGrid.Columns.Add(newColumn);

            //set a default title so we have something to display in all cases.
            SetTitle("(No Data Yet) - Exceptions");
        }

        /// <summary>
        /// Called to initialize the session view.
        /// </summary>
        /// <param name="context">A standard interface to the hosting environment for the view, specific to the repository where the session was opened from</param>
        /// <remarks>
        /// If any exception is thrown during this call this add in will not be loaded.
        /// </remarks>
        public void Initialize(ISessionContext context)
        {
        }

        /// <summary>
        /// Called to ask the view to refresh the data its display is based on.
        /// </summary>
        public void RefreshData()
        {
            //ask the grid to rebind to the current value..
            SetDisplaySession(m_Session);
        }

        /// <summary>
        /// Close the view and prepare to be released (object will still be disposed separately)  
        /// </summary>
        /// <param name="closeReason">The reason that the node is being closed.</param>
        /// <returns>
        /// True if the close was successful, false if it was unsuccessful or canceled by the user.
        /// </returns>
        public bool Close(CloseReason closeReason)
        {
            //we don't do anything special on close, we close without complaint or option to cancel.
            return true;
        }

        /// <summary>
        /// A display caption for the view (typically the Text property of a control or form)
        /// </summary>
        public string Title
        {
            get { return m_Title; } 
        }


        /// <summary>
        /// The session being displayed
        /// </summary>
        /// <remarks>
        /// This may be updated in some view contexts. It may also
        ///             be set to null to indicate the current session is being released. To fully
        ///             implement this interface it is necessary to be prepared for transitions
        ///             to a different session object and to or from null.
        /// </remarks>
        public ISession Session 
        { 
            get { return m_Session; }
            set { SetDisplaySession(value); }
        }

        /// <summary>
        /// Gets the icon to use for the view
        /// </summary>
        /// <remarks>
        /// This may return null, in which case the container will make the best
        /// </remarks>
        public Icon Icon
        {
            get { return SystemIcons.Error; }
        }

        #region Protected Properties and Methods

        protected virtual void OnTitleChanged()
        {
            EventHandler tempEvent = TitleChanged;

            if (tempEvent != null)
            {
                tempEvent.Invoke(this, new EventArgs());
            }
        }

        #endregion

        #region Private Properties and Methods

        /// <summary>
        /// Display the session, or clear the existing session.
        /// </summary>
        /// <param name="session"></param>
        private void SetDisplaySession(ISession session)
        {
            m_Session = session;

            if (m_Session == null)
            {
                //clear our current display
                exceptionsGrid.DataSource = null;
                SetTitle("(No Data Yet) - Exceptions");
            }
            else
            {
                //display the session
                BindingList<IExceptionInfo> exceptionsToDisplay = new BindingList<IExceptionInfo>();

                //find all exceptions in our log messages regardless of severity.
                foreach (ILogMessage message in m_Session.GetMessages())
                {
                    if (message.HasException)
                    {
                        //we want just the inner-most exception for this example.
                        IExceptionInfo innermostException = message.Exception;
                        while (innermostException.InnerException != null)
                        {
                            innermostException = innermostException.InnerException;
                        }
                        exceptionsToDisplay.Add(innermostException);
                    }
                }

                //now bind this list to the grid.
                exceptionsGrid.DataSource = exceptionsToDisplay;
                SetTitle(string.Format("{0} Exceptions", exceptionsToDisplay.Count));
            }
        }

        private void SetTitle(string newTitle)
        {
            //update the title and raise our title changed event.
            m_Title = newTitle;
            OnTitleChanged();
        }

        #endregion
    }
}
