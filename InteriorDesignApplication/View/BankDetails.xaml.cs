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
            if (addBankNamePanel.Visibility.Equals(Visibility.Collapsed))
            {
                if(!addBank())
                {
                    ErrorNotification.Text = "***Please fill up the required fields.***";
                    ErrorNotification.Visibility = Visibility.Visible; 
                }
            }
            else if (addBankNamePanel.Visibility.Equals(Visibility.Visible))
            {
                if(!addBankType())
                {
                    ErrorNotification.Text = "***Please fill up the required fields.***";
                    ErrorNotification.Visibility = Visibility.Visible;
                }
            }              
        }

        private bool addBank()
        {
            if (Branch.NoOfErrorsOnScreen == 0 &&
                AccountNo.NoOfErrorsOnScreen == 0 &&
                AccountName.NoOfErrorsOnScreen == 0 &&
                bankType.SelectedIndex != -1)
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
                return true;
            }
            return false;
        }

        private bool addBankType()
        {
            if (bankNameInput.NoOfErrorsOnScreen == 0)
            {
                bankNameInput.GetBindingExpression(TextBox.TextProperty).UpdateSource();               
                this.AddSaveBankBtn.SetBinding(Button.CommandProperty, new Binding("AddBankTypeCommand"));
                // call method for showing bank type panel here
                ShowBankFormPanel();
                return true;
            }
            return false;
        }

        private void addBankNameButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorNotification.Visibility = bankFormPanel.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = addBankNamePanel.Visibility = Visibility.Visible;
            bankTypeHeader.Text = "Add Bank Name";
            AddSaveButtonLabel.Text = "Add";
            bankNameInput.Text = string.Empty;
        }

        private void editBankNameButton_Click(object sender, RoutedEventArgs e)
        {
            bankNameInput.GetBindingExpression(TextBox.TextProperty).UpdateTarget();

            ErrorNotification.Visibility = bankFormPanel.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = addBankNamePanel.Visibility = Visibility.Visible;
            bankTypeHeader.Text = "Edit Bank Name";
            AddSaveButtonLabel.Text = "Update";
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            ShowBankFormPanel();
        }

        private void ShowBankFormPanel()
        {
            this.cancelButton.Visibility = this.addBankNamePanel.Visibility = Visibility.Collapsed;
            this.bankFormPanel.Visibility = Visibility.Visible;
        }

        private void bankNameComboxBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editBankNameButton.Visibility = Visibility.Visible;
        }

       
    }
}
