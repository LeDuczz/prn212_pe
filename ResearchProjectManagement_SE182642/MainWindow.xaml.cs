using ResearchProjectManagement.BLL.Services;
using ResearchProjectManagement.DAL.Entities;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResearchProjectManagement_SE182642
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ResearchProjectService _researchProjectService;
        private readonly ResearcherService _researcherService;

        public UserAccount CurrentUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _researchProjectService = new ResearchProjectService();
            _researcherService = new ResearcherService();
            LoadResearchProject();
            LoadResearchers();
        }

        public void LoadResearchProject()
        {
            dgResearchProject.ItemsSource = _researchProjectService.GetResearchProjects();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            var results = _researchProjectService.GetResearchProjects(searchText);
            dgResearchProject.ItemsSource = results;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(dgResearchProject.SelectedItem is ResearchProject researchProject)
            {
                var result = MessageBox.Show($"Are you sure you want to delete?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                _researchProjectService.DeleteResearchProject(researchProject);
                LoadResearchProject();
         
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ResearchProject researchProject = new ResearchProject()
            {
                ProjectId = ((ResearchProject)dgResearchProject.SelectedItem).ProjectId,
                ProjectTitle = txtProjectTitle.Text,
                ResearchField = txtResearchField.Text,
                StartDate = DateOnly.Parse(txtStartDate.Text),
                EndDate = DateOnly.Parse(txtEndDate.Text),
                LeadResearcherId = (int)cbxFullName.SelectedValue,
                Budget = decimal.Parse(txtBudget.Text),
            };
            _researchProjectService.UpdateResearchService(researchProject);
            LoadResearchProject();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            ResearchProject researchProject = new ResearchProject()
            {
                ProjectTitle = txtProjectTitle.Text,
                ResearchField = txtResearchField.Text,
                StartDate = DateOnly.Parse(txtStartDate.Text),
                EndDate = DateOnly.Parse(txtEndDate.Text),
                LeadResearcherId = (int)cbxFullName.SelectedValue,
                Budget = decimal.Parse(txtBudget.Text)
            };
            _researchProjectService.CreateResearchProject(researchProject);
            LoadResearchProject();
        }

        public void LoadResearchers()
        {
            cbxFullName.ItemsSource = _researcherService.GetResearchers();
            cbxFullName.DisplayMemberPath = "FullName";
            cbxFullName.SelectedValuePath = "ResearcherId";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(CurrentUser.Role == 3)
            {
                btnCreate.IsEnabled = false;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
            } else if(CurrentUser.Role == 2)
            {
                btnDelete.IsEnabled = false;
            }
        }

        private void dgResearchProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgResearchProject.SelectedItem is ResearchProject researchProject)
            {
                txtProjectTitle.Text = researchProject.ProjectTitle;
                txtResearchField.Text = researchProject.ResearchField;
                txtStartDate.Text = researchProject.StartDate.ToString("M/d/yyyy");
                txtEndDate.Text = researchProject.EndDate.ToString("M/d/yyyy");
                cbxFullName.SelectedValue = researchProject.LeadResearcherId;
                txtBudget.Text = researchProject.Budget.ToString();

            }
        }
    }
}