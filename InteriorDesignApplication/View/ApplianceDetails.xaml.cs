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
using Microsoft.Win32;

namespace View
{
    /// <summary>
    /// Interaction logic for ApplianceDetails.xaml
    /// </summary>
    public partial class ApplianceDetails : PopupView
    {
        private string view = string.Empty;
        public ApplianceDetails(string _view)
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
                WarrantyDate.SelectedDate = DateTime.Now;
            } 
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        IMainViewModel viewModel;

        private void AddSaveApplianceBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Description.NoOfErrorsOnScreen == 0 &&
                Model.NoOfErrorsOnScreen == 0)
            {
                if (view == "edit")
                {
                    this.AddSaveApplianceBtn.SetBinding(Button.CommandProperty, new Binding("EditUpdateApplianceCommand"));
                }
                else
                {
                    this.AddSaveApplianceBtn.SetBinding(Button.CommandProperty, new Binding("AddApplianceCommand"));
                }
                viewModel.CurrentPopupView = null;
            }
            else
            {
                ErrorNotification.Text = "Please fill up the required fields.";
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
                ApplianceReceipt.Text = dlg.FileName;
            }
        }
    }
}
