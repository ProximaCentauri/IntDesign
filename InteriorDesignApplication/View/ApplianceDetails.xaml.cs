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
    /// Interaction logic for ApplianceDetails.xaml
    /// </summary>
    public partial class ApplianceDetails : PopupView
    {
        public ApplianceDetails()
        {
            InitializeComponent();
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        IMainViewModel viewModel;

        private void AddSaveApplianceBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSaveApplianceBtn.SetBinding(Button.CommandProperty, new Binding("AddApplianceCommand"));
            viewModel.CurrentPopupView = null;
        }

        private void browsePayment_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
