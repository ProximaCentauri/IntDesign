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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;
using Model.Controls;
using Model;        

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for CompanyDetails.xaml
    /// </summary>
    public partial class CompanyInformation : UserControl
    {
        public CompanyInformation()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = this.DataContext as IMainViewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("NonAdmin") || viewModel.CurrentPopupView == null)
            {
                if (viewModel.CurrentAppUser != null)
                {
                    DisableControlForNonAdmin(viewModel.CurrentAppUser.IsAdmin);
                }
            } 
        }

        private void DisableControlForNonAdmin(bool isAdmin)
        {
            CompanyName.IsEnabled = Address.IsEnabled = ContactNumber.IsEnabled = Website.IsEnabled = EmailAddress.IsEnabled = FacebookPage.IsEnabled = Notes.IsEnabled = isAdmin;
        }

        IMainViewModel viewModel;
    }
}
