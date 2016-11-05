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
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        private void changePassBtn_Click(object sender, RoutedEventArgs e)
        {
            if(OldPassword.NoOfErrorsOnScreen != 0 ||
                NewPassword.NoOfErrorsOnScreen != 0 ||
                ConfirmPassword.NoOfErrorsOnScreen != 0)
            {
                warning.Text = "Please fill up the empty fields.";
                warning.Visibility = Visibility.Visible;
            }
        }
    }
}
