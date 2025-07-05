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
            string projectTitle = txtProjectTitle.Text.Trim();
            string researchField = txtResearchField.Text.Trim();
            DateTime? startDateValue = dpStartDate.SelectedDate;
            DateTime? endDateValue = dpEndDate.SelectedDate;
            string budgetText = txtBudget.Text.Trim();

            if (string.IsNullOrEmpty(projectTitle) ||
                string.IsNullOrEmpty(researchField) ||
                !startDateValue.HasValue ||
                !endDateValue.HasValue ||
                string.IsNullOrEmpty(budgetText) ||
                cbxFullName.SelectedValue == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateOnly startDate = DateOnly.FromDateTime(startDateValue.Value);
            DateOnly endDate = DateOnly.FromDateTime(endDateValue.Value);

            if (startDate >= endDate)
            {
                MessageBox.Show("StartDate must be earlier than EndDate.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (projectTitle.Length < 5 || projectTitle.Length > 100)
            {
                MessageBox.Show("ProjectTitle must be between 5 and 100 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            char firstChar = projectTitle[0];
            if (!char.IsUpper(firstChar) && !char.IsDigit(firstChar))
            {
                MessageBox.Show("ProjectTitle must start with a capital letter or a digit (1-9).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            char[] invalidChars = new[] { '$', '%', '^', '@' };
            foreach (char c in invalidChars)
            {
                if (projectTitle.Contains(c))
                {
                    MessageBox.Show("ProjectTitle cannot contain special characters such as $, %, ^, @.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            decimal budget = decimal.Parse(budgetText);
            int leadResearcherId = (int)cbxFullName.SelectedValue;
            ResearchProject researchProject = new ResearchProject()
            {
                ProjectId = ((ResearchProject)dgResearchProject.SelectedItem).ProjectId,
                ProjectTitle = projectTitle,
                ResearchField = researchField,
                StartDate = startDate,
                EndDate = endDate,
                LeadResearcherId = leadResearcherId,
                Budget = budget,
            };

            _researchProjectService.UpdateResearchService(researchProject);
            MessageBox.Show("Project updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadResearchProject();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string projectTitle = txtProjectTitle.Text.Trim();
            string researchField = txtResearchField.Text.Trim();
            DateTime? startDateValue = dpStartDate.SelectedDate;
            DateTime? endDateValue = dpEndDate.SelectedDate;
            string budgetText = txtBudget.Text.Trim();

            if (string.IsNullOrEmpty(projectTitle) ||
                string.IsNullOrEmpty(researchField) ||
                !startDateValue.HasValue ||
                !endDateValue.HasValue ||
                string.IsNullOrEmpty(budgetText) ||
                cbxFullName.SelectedValue == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateOnly startDate = DateOnly.FromDateTime(startDateValue.Value);
            DateOnly endDate = DateOnly.FromDateTime(endDateValue.Value);

            if (startDate >= endDate)
            {
                MessageBox.Show("StartDate must be earlier than EndDate.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (projectTitle.Length < 5 || projectTitle.Length > 100)
            {
                MessageBox.Show("ProjectTitle must be between 5 and 100 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            char firstChar = projectTitle[0];
            if (!char.IsUpper(firstChar) && !char.IsDigit(firstChar))
            {
                MessageBox.Show("ProjectTitle must start with a capital letter or a digit (1-9).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            char[] invalidChars = new[] { '$', '%', '^', '@' };
            foreach (char c in invalidChars)
            {
                if (projectTitle.Contains(c))
                {
                    MessageBox.Show("ProjectTitle cannot contain special characters such as $, %, ^, @.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            decimal budget = decimal.Parse(budgetText);
            int leadResearcherId = (int)cbxFullName.SelectedValue;

            ResearchProject researchProject = new ResearchProject
            {
                ProjectTitle = projectTitle,
                ResearchField = researchField,
                StartDate = startDate,
                EndDate = endDate,
                LeadResearcherId = leadResearcherId,
                Budget = budget
            };
            _researchProjectService.CreateResearchProject(researchProject);
            MessageBox.Show("Project created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                dpStartDate.SelectedDate = researchProject.StartDate.ToDateTime(TimeOnly.MinValue);
                dpEndDate.SelectedDate = researchProject.EndDate.ToDateTime(TimeOnly.MinValue);
                cbxFullName.SelectedValue = researchProject.LeadResearcherId;
                txtBudget.Text = researchProject.Budget.ToString();
            }
        }
    }
}