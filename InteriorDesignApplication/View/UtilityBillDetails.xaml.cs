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

namespace View
{
    /// <summary>
    /// Interaction logic for UtilityBillsDetails.xaml
    /// </summary>
    public partial class UtilityBillDetails : PopupView
    {
        public UtilityBillDetails()
        {
            InitializeComponent();
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
            this.viewModel.CreateEntity(new Utility());            
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
            if (this.addBillTypePanel.Visibility == Visibility.Visible ||
                this.addCompanyNamePanel.Visibility == Visibility.Visible)
            {                  
                ShowUtilityFormPanel();
            }           
            else if (this.utilityFormPanel.Visibility == Visibility.Visible)
            {                
                viewModel.CurrentPopupView = null;
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

        private void AddBillTypePanel_VisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                viewModel.CreateEntity(new UtilityBillType());
            }
        }

        private void AddCompanyPanel_VisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                viewModel.CreateEntity(new UtilityCompany());
            }
        }        
    }
}
