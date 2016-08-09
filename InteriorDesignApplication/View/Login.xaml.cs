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
                else
                {
                    PopupControl.ShowPopup(true, viewModel.CurrentPopupView, false);
                }
            }
        }
    }
}
