using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Loupe.Extension.FogBugz.Internal
{
    public partial class NewMappingDialog : Form
    {
        private const string AddMappingTitle = "Add a New Mapping";
        private const string EditMappingTitle = "Edit an Existing Mapping";

        private Dictionary<string, List<String>> m_ProductsAndApplications;
        private Dictionary<string, List<String>> m_ProjectsAndAreas;
        private Dictionary<int, string> m_Priorities;

        public NewMappingDialog()
        {
            InitializeComponent();
        }

        #region Public Properties and Methods

        public DialogResult AddMapping(Dictionary<string, List<String>> productsAndApplications, 
            Dictionary<string, List<String>> projectsAndAreas, 
            Dictionary<int, string> priorities, 
            out Mapping newMapping)
        {
            Text = AddMappingTitle;

            m_ProductsAndApplications = productsAndApplications;
            m_ProjectsAndAreas = projectsAndAreas;
            m_Priorities = priorities;

            newMapping = null;

            DisplayProductsAndApplications();
            DisplayPriorities();
            DisplayProjectsAndAreas();

            DialogResult result = ShowDialog();
            if (result == DialogResult.OK)
            {
                newMapping = new Mapping();
                UpdateData(newMapping);
            }

            return result;
        }

        public DialogResult EditMapping(Dictionary<string, List<String>> productsAndApplications, 
            Dictionary<string, List<String>> projectsAndAreas,
            Dictionary<int, string> priorities,
            Mapping existingMapping)
        {
            Text = EditMappingTitle;

            m_ProductsAndApplications = productsAndApplications;
            m_ProjectsAndAreas = projectsAndAreas;
            m_Priorities = priorities;

            DisplayProductsAndApplications();
            DisplayPriorities();
            DisplayProjectsAndAreas();

            //display the current mapping values
            ProductSelection.Text = existingMapping.Product;
            ApplicationSelection.Text = existingMapping.Application;
            txtVersions.Text = existingMapping.Versions;

            ProjectSelection.SelectedItem = existingMapping.Project;
            AreaSelection.SelectedItem = existingMapping.Area;
            PrioritySelection.SelectedValue = existingMapping.Priority;

            DialogResult result = ShowDialog();
            if (result == DialogResult.OK)
            {
                UpdateData(existingMapping);
            }

            return result;
        }

        #endregion

        #region Private Properties and Methods

        private void DisplayProductsAndApplications()
        {
            //now add these items to the project selection area.
            ProductSelection.DataSource = null;
            ProductSelection.Items.Clear();

            if (m_ProductsAndApplications != null)
            {
                ProductSelection.DataSource = new List<string>(m_ProductsAndApplications.Keys); //data source requires a list            
            }
        }

        private void DisplayPriorities()
        {
            //now add these items to the project selection area.
            PrioritySelection.DataSource = null;
            PrioritySelection.Items.Clear();

            if (m_Priorities != null)
            {
                PrioritySelection.DisplayMember = "Value";
                PrioritySelection.ValueMember = "Key";
                PrioritySelection.DataSource = new List<KeyValuePair<int, string>>(m_Priorities);  //data source requires a list    
            }
        }

        private void DisplayProjectsAndAreas()
        {
            //now add these items to the project selection area.
            ProjectSelection.DataSource = null;
            ProjectSelection.Items.Clear();

            if (m_ProjectsAndAreas != null)
            {
                ProjectSelection.DataSource = new List<string>(m_ProjectsAndAreas.Keys); //data source requires a list            
            }
        }

        private void UpdateData(Mapping mapping)
        {
            mapping.Product = (string)ProductSelection.SelectedItem ?? ProductSelection.Text;
            mapping.Application = (string)ApplicationSelection.SelectedItem ?? ApplicationSelection.Text;
            mapping.Versions = txtVersions.Text;
            mapping.Project = (string)ProjectSelection.SelectedItem;
            mapping.Area = (string)AreaSelection.SelectedItem;
            mapping.Priority = (int)PrioritySelection.SelectedValue;
        }

        private void ValidateData()
        {
            bool isValid = true;

            if (ProjectSelection.SelectedItem == null)
            {
                isValid = false;
            }

            if (AreaSelection.SelectedItem == null)
            {
                isValid = false;
            }

            if (PrioritySelection.SelectedItem == null)
            {
                isValid = false;
            }

            btnOK.Enabled = isValid;
        }

        #endregion

        #region Event Handlers

        private void ProductSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            //update our list of applications....
            List<string> applications;
            if ((ProductSelection.SelectedValue != null) && (m_ProductsAndApplications.TryGetValue((string)ProductSelection.SelectedValue, out applications)))
            {
                ApplicationSelection.DataSource = applications;
                ApplicationSelection.Enabled = ProjectSelection.Enabled;
            }
            else
            {
                ApplicationSelection.DataSource = null;
                ApplicationSelection.Enabled = false;
            }

            ValidateData();
        }

        private void ApplicationSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void ProjectSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            //update our list of areas....
            List<string> areas;
            if ((ProjectSelection.SelectedValue != null) && (m_ProjectsAndAreas.TryGetValue((string)ProjectSelection.SelectedValue, out areas)))
            {
                AreaSelection.DataSource = areas;
                AreaSelection.Enabled = ProjectSelection.Enabled;
            }
            else
            {
                AreaSelection.DataSource = null;
                AreaSelection.Enabled = false;
            }

            ValidateData();
        }

        private void AreaSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        #endregion
    }
}
