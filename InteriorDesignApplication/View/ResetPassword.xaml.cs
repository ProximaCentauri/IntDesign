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
using IntDesignControls;

namespace View
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : PopupView
    {
        IMainViewModel viewModel;

        public ResetPassword()
        {
            InitializeComponent();
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
            resetPassBtn.Text = "Submit";
        }

        private void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("InvalidEmailAdd"))
            {
                notification.Text = "Invalid email provided or temporary pin doesn't exist in our system.\nPlease input another email or temporary pin.";
                notification.Visibility = Visibility.Visible;
                showInputPanel(true);
            }
            else if (e.PropertyName.Equals("ValidEmailAdd"))
            {
                notification.Text = string.Empty;
                notification.Visibility = Visibility.Collapsed;
                showInputPanel(false);
                BindingOperations.ClearBinding(resetPassBtn, Button.CommandProperty);
            }
            else if (e.PropertyName.Equals("PasswordResetSuccessful"))
            {
                viewModel.CurrentPopupView = null;
            }
        }

        private void resetPassBtn_Click(object sender, RoutedEventArgs e)
        {
            if (inputPinPanel.Visibility.Equals(Visibility.Visible))
            {
                resetPassBtn.SetBinding(Button.CommandProperty, new Binding("VerifyResetPasswordCommand"));
                showInputPanel(false);
            }
            else
            {
                if (!NewPassword.PasswordText.Equals(ConfirmPassword.PasswordText))
                {
                    NewPassword.Text = NewPassword.PasswordText = ConfirmPassword.Text = ConfirmPassword.PasswordText = string.Empty;
                    notification.Visibility = Visibility.Visible;
                    notification.Text = "New password doesn't match the confirmation. Please re-enter password.";                    
                }
                else
                {
                    resetPassBtn.SetBinding(Button.CommandProperty, new Binding("ResetPasswordCommand"));
                    viewModel.CommandParameter = NewPassword.PasswordText;                    
                }                
            }            
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CommandParameter = string.Empty;
            viewModel.CurrentPopupView = null;
        }

        private void showInputPanel(bool show)
        {
            if (!show)
            {
                inputPinPanel.Visibility = Visibility.Collapsed;
                newPasswordPanel.Visibility = Visibility.Visible;
                resetPassBtn.Text = "Login";
            }
            else
            {
                inputPinPanel.Visibility = Visibility.Visible;
                newPasswordPanel.Visibility = Visibility.Collapsed;
                resetPassBtn.Text = "Submit";
            }
            
        }

        private void NewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                resetPassBtn_Click(this, null);
                if (resetPassBtn.Command != null)
                {
                    resetPassBtn.Command.Execute(null);
                }
                e.Handled = true;
            }
        }
    }
}
