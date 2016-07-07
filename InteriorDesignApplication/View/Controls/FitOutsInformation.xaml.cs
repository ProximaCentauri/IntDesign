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

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for FitOutsInformation.xaml
    /// </summary>
    public partial class FitOutsInformation : UserControl
    {
        public FitOutsInformation()
        {
            InitializeComponent();
        }

        private void addAppliance_Click(object sender, RoutedEventArgs e)
        {
            deleteEntry.Visibility = Visibility.Collapsed;
            viewModel.CurrentPopupView = new ApplianceDetails();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = this.DataContext as IMainViewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPopupView"))
            {

            }
        }

        IMainViewModel viewModel;

        private void gridAppliances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(gridAppliances.SelectedIndex >= 0)
                deleteEntry.Visibility = Visibility.Visible;
            else
                deleteEntry.Visibility = Visibility.Collapsed;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!EndDate.Text.Equals(string.Empty))
            {
                DateTime today = DateTime.Now;
                DateTime endDate = Convert.ToDateTime(EndDate.Text);
                DateTime warrantyEndDate = endDate.AddDays(45);
                int result = DateTime.Compare(today, warrantyEndDate);
                string warrantyStatus = string.Empty;

                if (result < 0)
                    warrantyStatus = "On Warranty";
                else
                    warrantyStatus = "Out of Warranty";

                WarrantyStatus.Text = warrantyStatus;
            }
            

        }
    }
}
