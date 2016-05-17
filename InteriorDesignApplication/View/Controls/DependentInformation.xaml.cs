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
using Model.Controls;
using ViewModel;
namespace View.Controls
{
    /// <summary>
    /// Interaction logic for DependentInformation.xaml
    /// </summary>
    public partial class DependentInformation : PopupView
    {
        public DependentInformation()
        {
            InitializeComponent();
        }

        private void PopupView_Loaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = (IMainViewModel)Application.Current.MainWindow.DataContext;     
        }

        IMainViewModel viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentPopupView = null;
        }

        
    }
}
