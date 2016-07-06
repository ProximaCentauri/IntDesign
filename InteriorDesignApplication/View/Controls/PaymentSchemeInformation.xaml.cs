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
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
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
            deleteEntry.Visibility = Visibility.Collapsed;
            viewModel.CurrentPopupView = new PaymentDetails();
        }

        private void gridPayments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(gridPayments.SelectedIndex >= 0)
                deleteEntry.Visibility = Visibility.Visible;
            else
                deleteEntry.Visibility = Visibility.Collapsed;
        }

        //private void TotalPayment_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    string unitCostText = UnitCost.Text;
        //    string totalPaymentText = TotalPayment.Text;

        //    if (unitCostText != string.Empty && totalPaymentText != string.Empty)
        //    {
        //        double unitCost = 0;
        //        double totalPayment = 0;
        //        double balance = 0;

        //        unitCost = Convert.ToDouble(unitCostText);
        //        totalPayment = Convert.ToDouble(totalPaymentText);
        //        balance = unitCost - totalPayment;

        //        Balance.Text = string.Format("{0:0.00}", balance.ToString());
        //    }
                
        //}
    }
}
