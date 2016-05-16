using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace View
{
    /// <summary>
    /// Interaction logic for DependentInformation.xaml
    /// </summary>
    public partial class DependentInformation : UserControl
    {
        public DependentInformation()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testpopup.IsOpen = false;           
        }
    }
}
