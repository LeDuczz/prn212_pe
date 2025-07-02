using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ResearchProjectManagement.BLL.Services;

namespace ResearchProjectManagement_SE182642
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserAccountService _userAccountService;
        public LoginWindow()
        {
            InitializeComponent();
            _userAccountService = new UserAccountService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            var account = _userAccountService.GetUserAccount(email, password);

            if (account != null)
            {
                if (account.Role != 4)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.CurrentUser = account;
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You have no permission to access this function!");
                }
            }
            else
            {
                MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
