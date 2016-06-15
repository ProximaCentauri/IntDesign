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
using System.Windows.Controls.Primitives;
using View;
using Model.Controls;
using ViewModel;
using Microsoft.Win32;
using Model;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for PersonalInformation.xaml
    /// </summary>
    public partial class PersonalInformation : UserControl
    {
        public PersonalInformation()
        {
            InitializeComponent();
            
        }

        private void addDependent_Click(object sender, RoutedEventArgs e)
        {
            deleteEntry.Visibility = Visibility.Collapsed;
            viewModel.CurrentPopupView = new DependentDetails();
        }

        IMainViewModel viewModel;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = this.DataContext as IMainViewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        public void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentPopupView"))
            {
                if (null == viewModel.CurrentPopupView)
                {
                    this.Opacity = 1;
                }
                else
                {
                    this.Opacity = 0.5;
                }
            }
            else if (e.PropertyName.Equals("SavingCustomer"))
            {
                viewModel.ReadyToSave = readyToSave;
            }            
        }

        private void CustomerImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (!String.IsNullOrEmpty(dlg.FileName))
            {
                this.viewModel.CustomerImageSource = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void gridDependents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridDependents.SelectedIndex >= 0)
                deleteEntry.Visibility = Visibility.Visible;
            else
                deleteEntry.Visibility = Visibility.Collapsed;
        }

        private void deleteEntry_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.CommandParameter = "DeleteDependent";
            Dependent customerDependent = gridDependents.SelectedItem as Dependent;
            viewModel.CurrentPopupView = new DeleteConfirmationPopup(customerDependent.FullName);
        }

        private bool readyToSave = true;
        
    }
}
