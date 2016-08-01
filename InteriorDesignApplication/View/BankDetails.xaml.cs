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
    /// Interaction logic for BankDetails.xaml
    /// </summary>
    public partial class BankDetails : PopupView
    {
        private string view = string.Empty;
        public BankDetails(string _view)
        {
            InitializeComponent();
            view = _view;
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
            if (view == "edit")
            {
                this.AddSaveButtonLabel.Text = "Update";
            }
            else
            {
                this.AddSaveButtonLabel.Text = "Add";
            }            
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        IMainViewModel viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Branch.NoOfErrorsOnScreen == 0 &&
                AccountNo.NoOfErrorsOnScreen == 0 &&
                AccountName.NoOfErrorsOnScreen == 0 &&
                BankName.NoOfErrorsOnScreen == 0)
            {
                if (view == "add")
                {
                    this.AddSaveBankBtn.SetBinding(Button.CommandProperty, new Binding("AddBankCommand"));
                }
                else
                {
                    this.AddSaveBankBtn.SetBinding(Button.CommandProperty, new Binding("EditUpdateBankCommand"));
                }

                viewModel.CurrentPopupView = null;
            }
            else
            {
                ErrorNotification.Text = "***Please fill up the required fields.***";
                ErrorNotification.Visibility = Visibility.Visible;
            }
            
        }

        private bool addBankType()
        {
            if (BankName.NoOfErrorsOnScreen == 0)
            {
                this.AddSaveBankBtn.SetBinding(Button.CommandProperty, new Binding("AddBankTypeCommand"));
                // call method for showing bank type panel here
                return true;
            }
            return false;
        }
    }
}
