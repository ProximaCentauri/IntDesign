﻿using System;
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
           this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }        

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.Load();
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPopupView"))
            {
                if (null == viewModel.CurrentPopupView)
                {
                    this.GridCustomers.IsEnabled = true;
                    this.GridCustomers.Opacity = this.MainTabControl.Opacity = this.SaveButton.Opacity = this.SearchPanel.Opacity = 1;
                    this.Opacity = 1;
                    PopupControl.ShowPopup(false, viewModel.CurrentPopupView, false);
                }
                else
                {
                    this.GridCustomers.IsEnabled = false;
                    this.GridCustomers.Opacity = this.MainTabControl.Opacity = this.SaveButton.Opacity = this.SearchPanel.Opacity = 0.5;                    
                    PopupControl.ShowPopup(true, viewModel.CurrentPopupView, false);
                }                
            }            
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
        
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;           
        }
   
        IMainViewModel viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (personalInfo.StatusIndex.Text.Equals("Married") && personalInfo.SpouseName.Text != String.Empty)
            {
                Spouse spouse = new Spouse();
                spouse.LastName = personalInfo.SpouseName.Text;
                viewModel.CreateEntity(spouse);
            }
        }
    }
}
