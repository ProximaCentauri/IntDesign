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
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using View;
using Model.Controls;
using ViewModel;

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
                    this.Background = Brushes.White;
                }
                else
                {
                    this.Opacity = .9;
                    this.Background = Brushes.Gray;
                }
            }
        }
    }
}
