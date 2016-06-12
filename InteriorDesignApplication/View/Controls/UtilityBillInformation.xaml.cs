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
using Model;
using System.Diagnostics;
using System.IO;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for UtilityBillInformation.xaml
    /// </summary>
    public partial class UtilityBillInformation : UserControl
    {
        public UtilityBillInformation()
        {
            InitializeComponent();
        }

        private void addUtilityBill_Click(object sender, RoutedEventArgs e)
        {
            deleteEntry.Visibility = editUtilityBill.Visibility = Visibility.Collapsed;
            viewModel.CurrentPopupView = new UtilityBillDetails("add");
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
        }

        private void editUtilityBill_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = new UtilityBillDetails("edit");
        }

        private void GridUtilityBills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridUtilityBills.SelectedIndex >= 0)
                deleteEntry.Visibility = editUtilityBill.Visibility = Visibility.Visible;
            else
                deleteEntry.Visibility = editUtilityBill.Visibility = Visibility.Collapsed;

        }

        private void deleteEntry_Click(object sender, RoutedEventArgs e)
        {
            string entryInfo = string.Empty;
            this.viewModel.CommandParameter = "DeleteUtilityBill";
            Utility utilityInfo = GridUtilityBills.SelectedItem as Utility;
            entryInfo = utilityInfo.BillType.Name + " bill (" + utilityInfo.UtilityCompany.Name + ") with account number: " + utilityInfo.AccountId;
            viewModel.CurrentPopupView = new DeleteConfirmationPopup(entryInfo);
        }

        IMainViewModel viewModel;

        private void BillStatementLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {            
            try
            {
                Process.Start(new ProcessStartInfo(@e.Uri.AbsolutePath)); 
            }
            catch(Exception ex)
            {

                // show pop-up here that problem is encountered opening file
            }
                    
        }

        private void OfficialReceiptLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(@e.Uri.AbsolutePath));
            }
            catch (Exception ex)
            {

                // show pop-up here that problem is encountered opening file
            }
        }


    }
}
