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

namespace View
{
    /// <summary>
    /// Interaction logic for GenericNotificationPopup.xaml
    /// </summary>
    public partial class WarningErrorNotificationPopup : PopupView
    {
        public WarningErrorNotificationPopup(string notificationText)
        {
            InitializeComponent();
            notification.Text = notificationText;
        }
        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
        }

         IMainViewModel viewModel;

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }
    }
}
