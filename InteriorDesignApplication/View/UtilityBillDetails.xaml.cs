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
using Model.Helpers;
using Microsoft.Win32;
using IntDesignControls;

namespace View
{
    /// <summary>
    /// Interaction logic for UtilityBillsDetails.xaml
    /// </summary>
    public partial class UtilityBillDetails : PopupView
    {
        private string view = string.Empty;        
        public UtilityBillDetails(string _view)
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
                UtilityCutoffDateTxt.SelectedDate = DateTime.Now;
            }                     
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            if (this.utilityFormPanel.Visibility == Visibility.Visible)
            {
                viewModel.CurrentPopupView = null;
            }
            else
            {
                ShowUtilityFormPanel();
            }
        }

        IMainViewModel viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!addUtility() &&
                addBillTypePanel.Visibility.Equals(Visibility.Collapsed) &&
                addCompanyNamePanel.Visibility.Equals(Visibility.Collapsed))                
            {
                ErrorNotification.Text = "***Please fill up the required fields.***";
                ErrorNotification.Visibility = Visibility.Visible; 
            }
            else if (addBillTypePanel.Visibility.Equals(Visibility.Visible) && !addBillType())
            {
                ErrorNotification.Text = "***Please fill up the required fields.***";
                ErrorNotification.Visibility = Visibility.Visible; 
            }
            else if (this.addCompanyNamePanel.Visibility == Visibility.Visible && !addCompany())
            {
                ErrorNotification.Text = "***Please fill up the required fields.***";
                ErrorNotification.Visibility = Visibility.Visible; 
            }
        }

        private bool addUtility()
        {
            if (SubscriberName.NoOfErrorsOnScreen == 0 &&
                AccountIdTxt.NoOfErrorsOnScreen == 0 &&
                utilityCompanyName.SelectedIndex != -1 &&
                billType.SelectedIndex != -1)
            {
                ErrorNotification.Visibility = Visibility.Collapsed;
                if (this.utilityFormPanel.Visibility == Visibility.Visible)
                {
                    if (view == "add")
                    {
                        this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("AddUtilityCommand"));
                    }
                    else if (view == "edit")
                    {
                        this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("EditUpdateUtilityCommand"));
                    }

                    viewModel.CurrentPopupView = null;
                    return true;
                }
            }
            return false;
        }

        private bool addBillType()
        {
            if (UtilityBillTypeInput.NoOfErrorsOnScreen == 0)
            {
                UtilityBillTypeInput.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("AddUtilityBillTypeCommand"));
                ShowUtilityFormPanel();
                return true;
            }
            return false;
        }

        private bool addCompany()
        {
            if (UtilityCompanyInput.NoOfErrorsOnScreen == 0 &&
                CompanyUtilityBillTypeSelection.SelectedIndex != -1)
            {
                UtilityCompanyInput.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                CompanyUtilityBillTypeSelection.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();

                this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("AddUtilityCompanyCommand"));
                ShowUtilityFormPanel();
                return true;
            }
            return false;
        }

        private void addBillType_Click(object sender, RoutedEventArgs e)
        {
            this.utilityFormPanel.Visibility = this.addCompanyNamePanel.Visibility= Visibility.Collapsed;
            this.cancelButton.Visibility = this.addBillTypePanel.Visibility = Visibility.Visible;
            ErrorNotification.Visibility = Visibility.Collapsed;
            billTypeHeader.Text = "Add Bill Type";
            this.AddSaveButtonLabel.Text = "Add";
            UtilityBillTypeInput.Text = string.Empty;
        }

        private void addCompanyName_Click(object sender, RoutedEventArgs e)
        {
            this.utilityFormPanel.Visibility = this.addBillTypePanel.Visibility = Visibility.Collapsed;
            this.cancelButton.Visibility = this.addCompanyNamePanel.Visibility = Visibility.Visible;
            ErrorNotification.Visibility = Visibility.Collapsed;
            companyNameHeader.Text = "Add Company Name";
            this.AddSaveButtonLabel.Text = "Update";
            UtilityCompanyInput.Text = string.Empty;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorNotification.Visibility = Visibility.Collapsed;
            ShowUtilityFormPanel();
        }

        private void ShowUtilityFormPanel()
        {
            this.cancelButton.Visibility = this.addCompanyNamePanel.Visibility = this.addBillTypePanel.Visibility = Visibility.Collapsed;
            this.utilityFormPanel.Visibility = Visibility.Visible;
        }
      

        private void browseBilling_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (!String.IsNullOrEmpty(dlg.FileName))
            {
                if (validateFileAttachment(dlg.SafeFileName, "bill"))
                {
                    BillStatementTxt.Text = dlg.FileName;
                }
                else
                {
                    BillStatementTxt.Text = string.Empty;
                    ErrorNotification.Visibility = Visibility.Visible;
                    ErrorNotification.Text = "The file name(Bill Statement) does not follow the correct format.";                    
                }
            }
        }

        private void browseReceipt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (!String.IsNullOrEmpty(dlg.FileName))
            {
                if (validateFileAttachment(dlg.SafeFileName, "receipt"))
                {
                    UtilityReceiptTxt.Text = dlg.FileName;
                }
                else
                {
                    UtilityReceiptTxt.Text = string.Empty;
                    ErrorNotification.Visibility = Visibility.Visible;
                    ErrorNotification.Text = "The file name(Receipt) does not follow the correct format.";
                }
            }
        }

        private void editBillType_Click(object sender, RoutedEventArgs e)
        {
            UtilityBillTypeInput.GetBindingExpression(TextBox.TextProperty).UpdateTarget();

            this.utilityFormPanel.Visibility = this.addCompanyNamePanel.Visibility = Visibility.Collapsed;
            this.cancelButton.Visibility = this.addBillTypePanel.Visibility = Visibility.Visible;
            billTypeHeader.Text = "Edit Bill Type";
            this.AddSaveButtonLabel.Text = "Update";
            ErrorNotification.Visibility = Visibility.Collapsed;
        }

        private void editCompanyName_Click(object sender, RoutedEventArgs e)
        {
            UtilityCompanyInput.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            CompanyUtilityBillTypeSelection.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateTarget();

            this.utilityFormPanel.Visibility = this.addBillTypePanel.Visibility = Visibility.Collapsed;
            this.cancelButton.Visibility = this.addCompanyNamePanel.Visibility = Visibility.Visible;
            companyNameHeader.Text = "Edit Company Name";
            this.AddSaveButtonLabel.Text = "Update";
            ErrorNotification.Visibility = Visibility.Collapsed;
        }

        private void billType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editBillType.Visibility = Visibility.Visible;
            billType.BorderBrush = Brushes.LightGray;
        }

        private void utilityCompanyName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editCompanyName.Visibility = Visibility.Visible;
            utilityCompanyName.BorderBrush = Brushes.LightGray;
        }        

        private bool validateFileAttachment(string filename, string attachmentType)
        {
            bool isValid = false;
            if (!String.IsNullOrEmpty(filename))
            {
                string[] names = filename.Split('_');                
                if (names.Count() == 4)
                {
                    isValid = true;
                    if (!names[0].ToLower().Equals(utilityCompanyName.Text.ToLower())
                                || !names[1].ToLower().Equals(AccountIdTxt.Text.ToLower())
                                || !names[2].Equals(UtilityCutoffDateTxt.SelectedDate.Value.ToString("MMddyyyy"))
                                || !names[3].ToLower().Remove(names[3].IndexOf('.')).Equals(attachmentType.ToLower()))
                    {
                        isValid = false;
                        BillStatementTxt.Background = Brushes.Red;
                    }                     
                }
                else
                {
                    isValid = false;
                }
            }            
            return isValid;
        }
    }
}
