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
using View;
using Model.Controls;
using ViewModel;
using System.Diagnostics;
using Model;
using System.IO;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for FitOutsInformation.xaml
    /// </summary>
    public partial class FitOutsInformation : UserControl
    {
        IMainViewModel viewModel;

        public FitOutsInformation()
        {
            InitializeComponent();
        }

        private void addAppliance_Click(object sender, RoutedEventArgs e)
        {
            deleteEntry.Visibility = Visibility.Collapsed;
            viewModel.CurrentPopupView = new ApplianceDetails("add");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = this.DataContext as IMainViewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPopupView"))
            {

            }

            if (viewModel.CurrentAppUser != null)
                DisableControlForNonAdmin(viewModel.CurrentAppUser.IsAdmin);
        }

        private void gridAppliances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(gridAppliances.SelectedIndex >= 0)
                deleteEntry.Visibility = editAppliance.Visibility = Visibility.Visible;
            else
                deleteEntry.Visibility = editAppliance.Visibility = Visibility.Collapsed;
        }
       
        private void editAppliance_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = new ApplianceDetails("edit");
        }

        private void OfficialReceiptLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            string notification = string.Empty;
            try
            {
                string file = @e.Uri.LocalPath.ToString();
                if (File.Exists(file))
                    Process.Start(new ProcessStartInfo(file));
                else
                {
                    notification = "A problem is encountered when trying to open file \"" + file + "\" \n\nCheck that: \n 1. File exists in the specified location \n 2. File is not corrupted";
                    viewModel.CurrentPopupView = new WarningErrorNotificationPopup(notification);
                }
            }
            catch (Exception ex)
            {
                viewModel.CurrentPopupView = new WarningErrorNotificationPopup(ex.Message);
            }

        }

        private void deleteEntry_Click(object sender, RoutedEventArgs e)
        {
            string entryInfo = string.Empty;
            this.viewModel.CommandParameter = "DeleteAppliance";
            Appliance applianceInfo = gridAppliances.SelectedItem as Appliance;
            entryInfo = applianceInfo.Description + " with model #: " + applianceInfo.ModelNumber;
            viewModel.CurrentPopupView = new DeleteConfirmationPopup(entryInfo);
        }

        private void DisableControlForNonAdmin(bool isAdmin)
        {
            FitOutCost.IsEnabled = FitOutBy.IsEnabled = Area.IsEnabled = StartDate.IsEnabled = EndDate.IsEnabled = isAdmin;
        }
    }
}
