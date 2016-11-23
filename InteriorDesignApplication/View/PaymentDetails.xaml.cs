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
using Microsoft.Win32;

namespace View
{
    /// <summary>
    /// Interaction logic for PaymentDetails.xaml
    /// </summary>
    public partial class PaymentDetails : PopupView
    {
        private string view = string.Empty;  
        public PaymentDetails(string _view)
        {
            InitializeComponent();
            view = _view;
            if (!view.Equals("edit"))
            {
                //PaymentDate.SelectedDate = DateTime.Parse(DateTime.Now.());
            }
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
            if (view == "edit")
            {
                this.AddSavePaymentBtn.Text = "Update";
            }
            else
            {
                this.AddSavePaymentBtn.Text = "Add";
                
            }   
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        IMainViewModel viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(cheque.NoOfErrorsOnScreen == 0 &&
                Amount.NoOfErrorsOnScreen == 0)
            {
                if (view == "edit")
                {
                    this.AddSavePaymentBtn.SetBinding(Button.CommandProperty, new Binding("EditUpdatePaymentCommand"));
                }
                else
                {
                    this.AddSavePaymentBtn.SetBinding(Button.CommandProperty, new Binding("AddPaymentCommand"));
                }
                viewModel.CurrentPopupView = null;
            }
            else
            {
                ErrorNotification.Text = "***Please fill up the required fields.***";
                ErrorNotification.Visibility = Visibility.Visible;
            }            
        }

        private void browsePayment_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (!String.IsNullOrEmpty(dlg.FileName))
            {
                paymentAttachment.Text = dlg.FileName;
            }
        }
    }
}
