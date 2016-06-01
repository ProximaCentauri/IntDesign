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
using Model;
using ViewModel;
using Model.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() { }
        public MainWindow(IMainViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
            viewModel.InitializeAndLoadEntities();            
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPopupView"))
            {
                if (null == viewModel.CurrentPopupView)
                {
                    this.GridCustomers.IsEnabled = true;
                    this.GridCustomers.Opacity = this.MainTabControl.Opacity = this.SaveButton.Opacity = this.DeleteButton.Opacity = this.SearchPanel.Opacity = 1;
                    this.Opacity = 1;
                    PopupControl.ShowPopup(false, viewModel.CurrentPopupView, false);
                }
                else
                {
                    this.GridCustomers.IsEnabled = false;
                    this.GridCustomers.Opacity = this.MainTabControl.Opacity = this.SaveButton.Opacity = this.DeleteButton.Opacity = this.SearchPanel.Opacity = 0.5;                    
                    PopupControl.ShowPopup(true, viewModel.CurrentPopupView, false);
                }                
            }            
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Search By: All");
            data.Add("First/Last Name");
            data.Add("Address");
            data.Add("Company Name");

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = data;
            comboBox.SelectedIndex = 0;
        }
        
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;
            DeleteButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (personalInfo.StatusIndex.Text.Equals("Married") && personalInfo.SpouseName.Text != String.Empty)
            {
                Spouse spouse = new Spouse();
                spouse.LastName = personalInfo.SpouseName.Text;
                spouse.BirthDate = personalInfo.SpouseBirthDate.SelectedDate;
                spouse.City = personalInfo.SpouseAddress.Text;
                viewModel.CreateEntity(spouse);                
            }            
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBlock.Focus();
            SearchTextBlock.Select(0, SearchTextBlock.Text.Length);
            DeleteButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.MainTabControl.SelectedIndex = 3;
        }

        IMainViewModel viewModel;

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = new DeleteConfirmationPopup();
        }

        private void GridCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(GridCustomers.SelectedIndex >= 0)
                DeleteButton.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
