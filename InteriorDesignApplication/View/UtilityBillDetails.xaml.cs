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
            if (EmptyFields())
            {
                ErrorNotification.Visibility = Visibility.Visible; 
                return;
            }

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
            }
            else if (this.addBillTypePanel.Visibility == Visibility.Visible)
            {
                this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("AddUtilityBillTypeCommand"));
                ShowUtilityFormPanel();
            }
            else if (this.addCompanyNamePanel.Visibility == Visibility.Visible)
            {
                this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("AddUtilityCompanyCommand"));
                ShowUtilityFormPanel();
            }
                                   
        }

        private bool EmptyFields()
        {
            bool retVal = false;    
            List<WatermarkTextBox> list = new List<WatermarkTextBox>();
            list.Add(SubscriberName);
            list.Add(AccountIdTxt);
            list.Add(UtilityReceiptTxt);
            list.Add(BillStatementTxt);
            CheckEmptyFields(list, out retVal);
            return retVal;
        }

        private void CheckEmptyFields(List<WatermarkTextBox> list, out bool retVal)
        {
            retVal = false;
            foreach (WatermarkTextBox textBox in list)
            {
                if(textBox.Text.Equals(textBox.Watermark)){
                    retVal = true;
                    textBox.WatermarkBorderColor = Brushes.Red;
                }   
            }
            if (utilityCompanyName.SelectedIndex == -1)
            {
                retVal = true;
                utilityCompanyName.BorderBrush = Brushes.Red;
                utilityCompanyName.Focus();
            }
            if (billType.SelectedIndex == -1)
            {
                retVal = true;
                billType.BorderBrush = Brushes.Red;
                billType.Focus();
            }
        }

        private void addBillType_Click(object sender, RoutedEventArgs e)
        {
            this.utilityFormPanel.Visibility = this.addCompanyNamePanel.Visibility= Visibility.Collapsed;
            this.cancelButton.Visibility = this.addBillTypePanel.Visibility = Visibility.Visible;
        }

        private void addCompanyName_Click(object sender, RoutedEventArgs e)
        {
            this.utilityFormPanel.Visibility = this.addBillTypePanel.Visibility = Visibility.Collapsed;
            this.cancelButton.Visibility = this.addCompanyNamePanel.Visibility = Visibility.Visible;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
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
                    string val = ErrorNotification.Text;
                    val += Environment.NewLine + "The file name(Bill Statement) does not follow the correct format.";
                    ErrorNotification.Text = val;
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
                    string val = ErrorNotification.Text;
                    val += Environment.NewLine + "The file name(Receipt) does not follow the correct format.";
                    ErrorNotification.Text = val;
                }
            }
        }

        private void editBillType_Click(object sender, RoutedEventArgs e)
        {
            this.utilityFormPanel.Visibility = this.addCompanyNamePanel.Visibility = Visibility.Collapsed;
            this.cancelButton.Visibility = this.addBillTypePanel.Visibility = Visibility.Visible;
            billTypeHeader.Text = "Edit Bill Type";
            this.AddSaveButtonLabel.Text = "Update";
        }

        private void editCompanyName_Click(object sender, RoutedEventArgs e)
        {
            this.utilityFormPanel.Visibility = this.addBillTypePanel.Visibility = Visibility.Collapsed;
            this.cancelButton.Visibility = this.addCompanyNamePanel.Visibility = Visibility.Visible;
            companyNameHeader.Text = "Edit Company Name";
            this.AddSaveButtonLabel.Text = "Update";
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
