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
using ViewModel;
using Model.Controls;
using Model;

namespace View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        IMainViewModel viewModel;

        public Login() { }
        public Login(IMainViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
            viewModel.InitializeAndLoadEntities();     
        }

        private void forgotPassBtn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CommandParameter = "forgotPassword";
            viewModel.CurrentPopupView = new ForgotPassword();            
        }

        private void resetPassBtn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = new ResetPassword();
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPopupView"))
            {
                if (null == viewModel.CurrentPopupView)
                {
                    PopupControl.ShowPopup(false, viewModel.CurrentPopupView, false);
                }
                else if (viewModel.CommandParameter != null && viewModel.CommandParameter.Equals("forgotPassword"))
                {
                    PopupControl.ShowPopup(true, viewModel.CurrentPopupView, false);
                }             
            }
            else if (e.PropertyName.Equals("InvalidUser"))
            {
                loginWarning.Visibility = Visibility.Visible;
            }
            else if (e.PropertyName.Equals("LogoutUser"))
            {
                loginWarning.Visibility = Visibility.Collapsed;
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(password.NoOfErrorsOnScreen != 0 ||
                userName.NoOfErrorsOnScreen != 0)
            {
                loginWarning.Visibility = Visibility.Visible;
            }
            else
            {
                this.userName.Text = string.Empty;
                this.password.Text = string.Empty;
                loginWarning.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
