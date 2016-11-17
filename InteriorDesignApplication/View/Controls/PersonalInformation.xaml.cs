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
using System.Windows.Controls.Primitives;
using View;
using Model.Controls;
using ViewModel;
using Microsoft.Win32;
using Model;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for PersonalInformation.xaml
    /// </summary>
    public partial class PersonalInformation : UserControl
    {
        public PersonalInformation()
        {
            InitializeComponent();            
        }

        private void addDependent_Click(object sender, RoutedEventArgs e)
        {
            deleteEntry.Visibility = Visibility.Collapsed;
            viewModel.CurrentPopupView = new DependentDetails();
        }

        IMainViewModel viewModel;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = this.DataContext as IMainViewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged;            
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPopupView"))
            {
                this.Opacity = null == viewModel.CurrentPopupView ? 1 : 0.5;                
            }
            else if (e.PropertyName.Equals("SavingCustomer"))
            {
                viewModel.ReadyToSave = LastName.NoOfErrorsOnScreen == 0 && FirstName.NoOfErrorsOnScreen == 0;                
            }
            else if (e.PropertyName.Equals("FileNotFound"))
            {
                string notification = string.Empty;
                notification = "A problem is encountered when trying to open customer's profile image. \n\nCheck that: \n 1. File exists in the specified location \n 2. File is not corrupted";
                viewModel.CurrentPopupView = new WarningErrorNotificationPopup(notification);
            }

            if(e.PropertyName.Equals("NonAdmin") || viewModel.CurrentPopupView == null)
            {
                if (viewModel.CurrentAppUser != null)
                {
                    DisableControlForNonAdmin(viewModel.CurrentAppUser.IsAdmin);
                }                
            }                
        }

        private void DisableControlForNonAdmin(bool isAdmin)
        {
            LastName.IsEnabled = FirstName.IsEnabled = MiddleName.IsEnabled = Religion.IsEnabled = Nationality.IsEnabled = ValidId.IsEnabled = CustomerImageBtn.IsEnabled =
                Birthdate.IsEnabled = Birthplace.IsEnabled = StatusIndex.IsEnabled = Gender.IsEnabled = addDependent.IsEnabled = gridDependents.IsEnabled = MobileNum.IsEnabled =
                LandlineNum.IsEnabled = EmailAd.IsEnabled = BuildingNum.IsEnabled = Street.IsEnabled = Village.IsEnabled = City.IsEnabled = State.IsEnabled = Country.IsEnabled =
                ZipCode.IsEnabled = isAdmin;
        }

        private void CustomerImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (!String.IsNullOrEmpty(dlg.FileName))
            {
                this.viewModel.CustomerImageSource = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void gridDependents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridDependents.SelectedIndex >= 0)
                deleteEntry.Visibility = Visibility.Visible;
            else
                deleteEntry.Visibility = Visibility.Collapsed;
        }

        private void deleteEntry_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.CommandParameter = "DeleteDependent";
            Dependent customerDependent = gridDependents.SelectedItem as Dependent;
            viewModel.CurrentPopupView = new DeleteConfirmationPopup(customerDependent.FullName);
        }
    }
}
