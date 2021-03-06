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
using System.Windows.Shapes;
using ViewModel;
using Model.Controls;
using Model;
namespace View
{
    /// <summary>
    /// Interaction logic for DependentDetails.xaml
    /// </summary>
    public partial class DependentDetails : PopupView
    {
        public DependentDetails()
        {
            InitializeComponent();
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;            
            this.viewModel.CreateEntity(new Dependent());
        }
        
        private void close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        IMainViewModel viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastName.NoOfErrorsOnScreen == 0 && FirstName.NoOfErrorsOnScreen == 0)      
            {
                viewModel.ReadyToSave = true;
                viewModel.CurrentPopupView = null;
            }
            else
            {
                ErrorNotification.Visibility = Visibility.Visible;
                viewModel.ReadyToSave = false;
            }
            //else
            //{
            //    ErrorNotification.Visibility = Visibility.Visible;
            //}            
        }        
    }
}
