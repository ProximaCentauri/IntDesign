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

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (LoginBtn.Command != null)
                {
                    LoginBtn_Click(this, null);
                    LoginBtn.Command.Execute(null);                    
                }
                e.Handled = true;
            }
        }

        private void forgotPassBtn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CommandParameter = "forgotPassword";
            viewModel.CurrentPopupView = new ForgotPassword();            
        }

        private void resetPassBtn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CommandParameter = "resetPassword";
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
                else if (viewModel.CommandParameter != null && (viewModel.CommandParameter.Equals("forgotPassword") || 
                    viewModel.CommandParameter.Equals("resetPassword")) ||
                    viewModel.CommandParameter.Equals("TemporaryPINSent"))
                {
                    PopupControl.ShowPopup(true, viewModel.CurrentPopupView, false);
                }
            }
            else if (e.PropertyName.Equals("TemporaryPINAlreadySent"))
            {
                viewModel.CurrentPopupView = new SaveNotificationPopup("Please check your email. Your temporary password/PIN has been sent to your email.\nClick on Reset Password? button in the login screen to reset password using the temporary password/PIN provided.");
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
            userName.Focusable = true;
            Keyboard.Focus(userName);
            if(password.NoOfErrorsOnScreen == 0 &&
                userName.NoOfErrorsOnScreen == 0)
            {
                viewModel.CommandParameter = password.PasswordText;
                this.userName.Text = string.Empty;
                this.password.Text = string.Empty;
                this.password.PasswordText = string.Empty;
                loginWarning.Visibility = Visibility.Collapsed;                
            }
            else
            {                
                loginWarning.Visibility = Visibility.Visible;                
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userName.Focusable = true;
            Keyboard.Focus(userName);
        }
    }
}
