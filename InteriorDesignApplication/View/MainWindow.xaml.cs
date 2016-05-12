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

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Search By:");
            data.Add("First Name");
            data.Add("Last Name");
            data.Add("Address");
            data.Add("Phone Number");

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = data;
            comboBox.SelectedIndex = 0;
        }

        //private void BtnSaveDetails_Click(object sender, RoutedEventArgs e)
        //{
        //    //Customer customer = new Customer();
        //    //customer.FirstName = "Julie Ann";
        //    //customer.LastName = "Lasala";
        //    //customer.Age = 29;
        //    //Dependent dependent = new Dependent();
        //    //dependent.FirstName="Tristan Jared";
        //    //customer.Dependents.Add(dependent);
        //    //viewModel.AddCustomer(customer);
        //    Customer customer = new Customer();
        //    customer.FirstName = tbFirstName.Text;
        //    customer.LastName = tbLastName.Text;
        //    customer.MiddleName = tbMiddleName.Text;
        //    viewModel.ActionCommand.Execut
        //}
      
    }
}
