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
using View;
using Model.Controls;
using ViewModel;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for PaymentSchemeInformation.xaml
    /// </summary>
    public partial class PaymentSchemeInformation : UserControl
    {
        public PaymentSchemeInformation()
        {
            InitializeComponent();
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

        private void addPayment_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = new PaymentDetails();
        }
    }
}