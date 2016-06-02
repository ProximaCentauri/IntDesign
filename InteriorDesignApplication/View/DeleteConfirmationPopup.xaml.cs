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
using ViewModel;
using Model.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for DeleteConfirmationPopup.xaml
    /// </summary>
    public partial class DeleteConfirmationPopup : PopupView
    {
        string entryValue = string.Empty;
        public DeleteConfirmationPopup(string _entryValue)
        {
            InitializeComponent();
            entryValue = _entryValue;
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;    
            entryValueText.Text = String.Format("Are you sure you want to delete: {0} ", entryValueText);
        }

        IMainViewModel viewModel;

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {           
            viewModel.CurrentPopupView = null;
        }
    }
}
