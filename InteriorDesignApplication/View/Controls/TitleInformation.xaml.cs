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
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for TitleInformation.xaml
    /// </summary>
    public partial class TitleInformation : UserControl
    {
        public TitleInformation()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
            
            if(viewModel.CurrentSelectedCustomer != null)
                addressInfo.Text = viewModel.CurrentSelectedCustomer.Address;
        }

        private void browseBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (!String.IsNullOrEmpty(dlg.FileName))
            {
               ScannedTitleText.Text = dlg.FileName;
            }
        }

        IMainViewModel viewModel;

        private void ScannedTitleLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
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
