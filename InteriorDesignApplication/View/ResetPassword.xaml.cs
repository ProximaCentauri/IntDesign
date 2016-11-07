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
            resetPassBtn.Text = "Submit";
        }

        private void resetPassBtn_Click(object sender, RoutedEventArgs e)
        {
            inputPinPanel.Visibility = Visibility.Collapsed;
            newPasswordPanel.Visibility = Visibility.Visible;
            resetPassBtn.Text = "Login";
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CommandParameter = "";
            viewModel.CurrentPopupView = null;
        }
    }
}
