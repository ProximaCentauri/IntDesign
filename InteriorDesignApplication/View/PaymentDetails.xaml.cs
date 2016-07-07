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
            if (view == "edit")
            {
                this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("EditUpdatePaymentCommand"));
            }
            else
            {
                this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("AddPaymentCommand"));
            }
            viewModel.CurrentPopupView = null;
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
