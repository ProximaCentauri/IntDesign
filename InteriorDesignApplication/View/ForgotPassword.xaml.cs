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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : PopupView
    {
        IMainViewModel viewModel;

        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        private void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("TemporaryPINSendFailed"))
            {
                notification.Text = "Problem encountered while sending the temporary pin.\nPlease make sure you have an internet connection before doing this task.";
                notification.Visibility = Visibility.Visible;
                generatePinBtn.Visibility = Visibility.Visible;
            }
            else if (e.PropertyName.Equals("TemporaryPINSent"))
            {
                notification.Text = "Please check your email. Your temporary password/PIN has been sent to your email. Click on Reset Password? button in the login screen to reset password using the temporary password/PIN provided.";
                notification.Visibility = Visibility.Visible;
                generatePinBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                notification.Visibility = Visibility.Collapsed;
                notification.Text = string.Empty;
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CommandParameter = "";
            viewModel.CurrentPopupView = null;
        }

        private void generatePinBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
