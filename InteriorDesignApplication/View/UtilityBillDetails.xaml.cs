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
using Model.Helpers;
using Microsoft.Win32;

namespace View
{
    /// <summary>
    /// Interaction logic for UtilityBillsDetails.xaml
    /// </summary>
    public partial class UtilityBillDetails : PopupView
    {
        private string view = string.Empty;
        public UtilityBillDetails(string _view)
        {
            InitializeComponent();
            view = _view;
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;
            if (view == "edit")
            {
                this.AddSaveButtonLabel.Text = "Update";
            }
            else
            {
                this.AddSaveButtonLabel.Text = "Add";
            }                     
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
            if (this.utilityFormPanel.Visibility == Visibility.Visible)
            {
                if (view == "add")
                {
                    this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("AddUtilityCommand"));
                }
                else if (view == "edit")
                {
                    this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("EditUpdateUtilityCommand"));
                }
                viewModel.CurrentPopupView = null;
            }
            else if (this.addBillTypePanel.Visibility == Visibility.Visible)
            {
                this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("AddUtilityBillTypeCommand"));
                ShowUtilityFormPanel();
            }
            else if (this.addCompanyNamePanel.Visibility == Visibility.Visible)
            {
                this.AddSaveUtilityBtn.SetBinding(Button.CommandProperty, new Binding("AddUtilityCompanyCommand"));
                ShowUtilityFormPanel();
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

        private void browseBilling_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (!String.IsNullOrEmpty(dlg.FileName))
            {
                BillStatementTxt.Text = dlg.FileName;                
            }
        }

        private void browseReceipt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (!String.IsNullOrEmpty(dlg.FileName))
            {
                UtilityReceiptTxt.Text = dlg.FileName;
            }
        }

        private void editBillType_Click(object sender, RoutedEventArgs e)
        {
            this.utilityFormPanel.Visibility = this.addCompanyNamePanel.Visibility = Visibility.Collapsed;
            this.cancelButton.Visibility = this.addBillTypePanel.Visibility = Visibility.Visible;
            billTypeHeader.Text = "Edit Bill Type";
            this.AddSaveButtonLabel.Text = "Update";
        }

        private void editCompanyName_Click(object sender, RoutedEventArgs e)
        {
            this.utilityFormPanel.Visibility = this.addBillTypePanel.Visibility = Visibility.Collapsed;
            this.cancelButton.Visibility = this.addCompanyNamePanel.Visibility = Visibility.Visible;
            companyNameHeader.Text = "Edit Company Name";
            this.AddSaveButtonLabel.Text = "Update";
        }

        private void billType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editBillType.Visibility = Visibility.Visible;
        }

        private void utilityCompanyName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editCompanyName.Visibility = Visibility.Visible;
        }        
    }
}
