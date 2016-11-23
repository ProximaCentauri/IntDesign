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

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for BankInformation.xaml
    /// </summary>
    public partial class BankInformation : UserControl
    {
        IMainViewModel viewModel;

        public BankInformation()
        {
            InitializeComponent();
        }

        private void addBankDetails_Click(object sender, RoutedEventArgs e)
        {
            deleteEntry.Visibility = Visibility.Collapsed;
            viewModel.CurrentPopupView = new BankDetails("add");
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

        private void gridBanks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridBanks.SelectedIndex >= 0)
            {
                editBankInfo.Visibility = deleteEntry.Visibility = Visibility.Visible;
            }
            else
            {
                editBankInfo.Visibility = deleteEntry.Visibility = Visibility.Collapsed;
            }
            
        }

        private void editBankInfo_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = new BankDetails("edit");
        }        

        private void deleteEntry_Click(object sender, RoutedEventArgs e)
        {
            string bankInfo = string.Empty;
            this.viewModel.CommandParameter = "DeleteBankInfo";
            Bank selectedBank = gridBanks.SelectedItem as Bank;
            bankInfo = selectedBank.BankType.Name + " with account number: " + selectedBank.AccountNumber;
            viewModel.CurrentPopupView = new DeleteConfirmationPopup(bankInfo);
        }

    }
}
