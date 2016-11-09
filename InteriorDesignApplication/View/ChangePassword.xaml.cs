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
using Model.Helpers;

namespace View
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : PopupView
    {
        IMainViewModel viewModel;

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;            
            viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("PasswordChangeSuccessful"))
            {
                viewModel.CurrentPopupView = null;
            }
        }             

        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        private void changePassBtn_Click(object sender, RoutedEventArgs e)
        {
            warning.Text = string.Empty;
            warning.Visibility = Visibility.Collapsed;

            if(!OldPassword.PasswordText.Equals(string.Empty) &&
                !NewPassword.PasswordText.Equals(string.Empty) &&
                !ConfirmPassword.PasswordText.Equals(string.Empty))
            {
                string plainPwd = DataEncryptor.Decrypt(viewModel.CurrentAppUser.CurrentPassword);
                if (!NewPassword.PasswordText.Equals(ConfirmPassword.PasswordText))
                {
                    ShowMessage("New password doesn't match the confirmation. Please re-enter password.");
                }
                else if (!OldPassword.PasswordText.Equals(plainPwd))
                {
                    ShowMessage("Old password doesn't match the current password. Please re-enter password.");
                }
                else if (NewPassword.PasswordText.Equals(plainPwd))
                {
                    ShowMessage("Old password is the same with new password. Please re-enter new and unique password.");
                }
                else
                {
                    viewModel.CommandParameter = NewPassword.PasswordText;
                    this.changePassBtn.SetBinding(Button.CommandProperty, new Binding("ChangeUserPasswordCommand"));
                }
            }
            else
            {
                ShowMessage("Please fill up the empty fields.");                
            }
        }

        private void ShowMessage(string msg)
        {
            warning.Text = msg;
            warning.Visibility = Visibility.Visible;
            OldPassword.Text = NewPassword.Text = ConfirmPassword.Text = string.Empty;            
        }
    }
}
