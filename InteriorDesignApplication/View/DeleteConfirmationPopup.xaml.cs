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

namespace View
{
    /// <summary>
    /// Interaction logic for DeleteConfirmationPopup.xaml
    /// </summary>
    public partial class DeleteConfirmationPopup : PopupView
    {
        string entryValue = string.Empty;
        public DeleteConfirmationPopup(string _entryValue)
        {
            InitializeComponent();
            entryValue = _entryValue;
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
            if (viewModel.CommandParameter.Equals("DeleteCustomer"))
            {
                entryValueText.Text = String.Format("Are you sure you want to delete customer: {0} ?", entryValue);
            }
            else if (viewModel.CommandParameter.Equals("DeleteDependent"))
            {
                entryValueText.Text = String.Format("Are you sure you want to delete dependent: {0}? ", entryValue);
            }
            else if (viewModel.CommandParameter.Equals("DeleteBankInfo"))
            {
                entryValueText.Text = String.Format("Are you sure you want to delete bank info: {0}? ", entryValue);
            }
            else if (viewModel.CommandParameter.Equals("DeleteUtilityBill"))
            {
                entryValueText.Text = String.Format("Are you sure you want to delete utility info: {0}? ", entryValue);
            }   
            else if(viewModel.CommandParameter.Equals("DeletePayment"))
            {
                entryValueText.Text = String.Format("Are you sure you want to delete payment: {0}? ", entryValue);
            }
            else if (viewModel.CommandParameter.Equals("DeleteAppliance"))
            {
                entryValueText.Text = String.Format("Are you sure you want to delete appliance: {0}? ", entryValue);
            }
            
      
        }

        IMainViewModel viewModel;

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            //if (viewModel.CommandParameter.Equals("DeleteCustomer"))
            //{                
            //    deleteButton.SetBinding(Button.CommandProperty, new Binding("DeleteCustomerCommand"));
            //}
            //else if (viewModel.CommandParameter.Equals("DeleteDependent"))
            //{
            //    deleteButton.SetBinding(Button.CommandProperty, new Binding("DeleteDependentCommand"));
            //}
            //else if (viewModel.CommandParameter.Equals("DeleteBankInfo"))
            //{
            //    deleteButton.SetBinding(Button.CommandProperty, new Binding("DeleteBankCommand"));
            //}
            //else if (viewModel.CommandParameter.Equals("DeleteUtilityBill"))
            //{
            //    deleteButton.SetBinding(Button.CommandProperty, new Binding("DeleteUtilityCommand"));
            //}
            //else if(viewModel.CommandParameter.Equals("DeletePayment"))
            //{
            //    deleteButton.SetBinding(Button.CommandProperty, new Binding("DeletePaymentCommand"));
            //}
            //else if (viewModel.CommandParameter.Equals("DeleteAppliance"))
            //{
            //    deleteButton.SetBinding(Button.CommandProperty, new Binding("DeleteApplianceCommand"));
            //}

            deleteButton.CommandParameter = viewModel.CommandParameter;
            viewModel.CurrentPopupView = null;
        }
    }
}
