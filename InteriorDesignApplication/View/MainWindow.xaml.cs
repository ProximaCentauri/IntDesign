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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            enableCustomerFormPanel(false);
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPopupView"))
            {
                if (null == viewModel.CurrentPopupView)
                {
                    this.GridCustomers.IsEnabled = true;
                    this.userInfoPanel.Opacity = this.GridCustomers.Opacity = this.MainTabControl.Opacity = this.CancelButton.Opacity = this.SaveButton.Opacity = this.DeleteButton.Opacity = this.SearchPanel.Opacity = 1;
                    this.Opacity = 1;
                    PopupControl.ShowPopup(false, viewModel.CurrentPopupView, false);
                }
                else
                {
                    this.GridCustomers.IsEnabled = false;
                    this.userInfoPanel.Opacity = this.GridCustomers.Opacity = this.MainTabControl.Opacity = this.CancelButton.Opacity = this.SaveButton.Opacity = this.DeleteButton.Opacity = this.SearchPanel.Opacity = 0.5;                    
                    PopupControl.ShowPopup(true, viewModel.CurrentPopupView, false);
                }
            }
            else if (e.PropertyName.Equals("SavedCustomer"))
            {   
                viewModel.CurrentPopupView = new SaveNotificationPopup("Details are successfully saved.");
            }
            else if (e.PropertyName.Equals("EmptyFields"))
            {
                viewModel.CurrentPopupView = new WarningErrorNotificationPopup("Please fill the required fields.");
            }
            else if (e.PropertyName.Equals("ReadyToSave"))
            {
                SaveDetails();
            }
            else if (e.PropertyName.Equals("PasswordChanged"))
            {
                viewModel.CurrentPopupView = new SaveNotificationPopup("Password changed successfully.");
            }

            if (e.PropertyName.Equals("NonAdmin") || viewModel.CurrentPopupView == null)
            {
                if (viewModel.CurrentAppUser != null)
                {
                    DisableControlForNonAdmin(viewModel.CurrentAppUser.IsAdmin);
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
            addCustomer = true;
            enableCustomerFormPanel(true);
            DeleteButton.Visibility = Visibility.Collapsed;
        }

        private void SaveDetails()
        {
            personalInfo.LastName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            personalInfo.FirstName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
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
            DeleteButton.Visibility = Visibility.Collapsed;
            if(GridCustomers.SelectedIndex < 0)
            {
                enableCustomerFormPanel(false);
            }
            else
            {
                enableCustomerFormPanel(true);
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.MainTabControl.SelectedIndex = 3;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.CommandParameter = "DeleteCustomer";
            viewModel.CurrentPopupView = new DeleteConfirmationPopup(viewModel.CurrentSelectedCustomer.FullName);
        }

        private void GridCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridCustomers.SelectedIndex >= 0)
            {
                DeleteButton.Visibility = Visibility.Visible;
                enableCustomerFormPanel(true);                               
            }
            else
            {
                DeleteButton.Visibility = Visibility.Collapsed;
                if(!addCustomer)
                    enableCustomerFormPanel(false);
            }
            addCustomer = false;
        }

        private void searchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchComboBox.SelectedIndex == 0)
            {
                SearchTextBlock.Text = string.Empty;
                SearchTextBlock.IsEnabled = false;
            }
            else
                SearchTextBlock.IsEnabled = true;
        }

        private void enableCustomerFormPanel(bool enable)
        {
            
            if(enable)
            {
                MainTabControl.IsEnabled = CancelButton.IsEnabled = SaveButton.IsEnabled = true;
                MainTabControl.SelectedIndex = 0;
                MainTabControl.Opacity = CancelButton.Opacity = SaveButton.Opacity = 1;
                personalInfo.LastName.Focus();
            }
            else
            {
                MainTabControl.IsEnabled = CancelButton.IsEnabled = SaveButton.IsEnabled = false;
                MainTabControl.SelectedIndex = 0;
                MainTabControl.Opacity = CancelButton.Opacity = SaveButton.Opacity = 0.50;
            }
        }

        IMainViewModel viewModel;
        private bool addCustomer = false;

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = new CancelConfirmationPopup();
        }

        private void changePassBtn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = new ChangePassword();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (logOutBtn.Command != null)
            {
                logOutBtn.Command.Execute("LogoutUserCommand");
            }
            viewModel.PropertyChanged -= viewModel_PropertyChanged;            
        }

        private void DisableControlForNonAdmin(bool isAdmin)
        {
            AddCustomerButton.IsEnabled = DeleteButton.IsEnabled = isAdmin;
            DeleteButton.Opacity = 0.50;
            BankInfoTab.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
