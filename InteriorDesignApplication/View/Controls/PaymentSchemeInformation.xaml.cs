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
    /// Interaction logic for PaymentSchemeInformation.xaml
    /// </summary>
    public partial class PaymentSchemeInformation : UserControl
    {
        IMainViewModel viewModel;

        public PaymentSchemeInformation()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPopupView"))
            {

            }

            if (e.PropertyName.Equals("NonAdmin") || viewModel.CurrentPopupView == null)
            {
                TotalPayment.IsEnabled = Balance.IsEnabled = false;

                if (viewModel.CurrentAppUser != null)
                {
                    DisableControlForNonAdmin(viewModel.CurrentAppUser.IsAdmin);
                }
            } 
        }       

        private void addPayment_Click(object sender, RoutedEventArgs e)
        {
            deleteEntry.Visibility = Visibility.Collapsed;
            viewModel.CurrentPopupView = new PaymentDetails("add");
        }

        private void gridPayments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridPayments.SelectedIndex >= 0)
            {
                editPayment.Visibility = deleteEntry.Visibility = Visibility.Visible;                
            }
            else
            {
                editPayment.Visibility = deleteEntry.Visibility = Visibility.Collapsed;
            }
        }

        private void editPayment_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = new PaymentDetails("edit");
        }

        private void AttachedFileLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
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
            this.viewModel.CommandParameter = "DeletePayment";
            Payment paymentInfo = gridPayments.SelectedItem as Payment;
            entryInfo = "check number " + paymentInfo.ChequeNumber + " with the amount of " + string.Format("{0:0,0.00}", paymentInfo.Amount);
            viewModel.CurrentPopupView = new DeleteConfirmationPopup(entryInfo);            
        }

        private void DisableControlForNonAdmin(bool isAdmin)
        {
            UnitCost.IsEnabled = isAdmin;
        }
    }
}
