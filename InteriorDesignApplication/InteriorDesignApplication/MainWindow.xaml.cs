using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Data.Entity;

namespace InteriorDesignApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private readonly ManagerDBContext _context = new ManagerDBContext();

        private IEnumerable<Customer> _customers;
        public IEnumerable<Customer> Customers
        {
            get
            {
                return new ObservableCollection<Customer>(_context.Customers.Include(d=>d.Dependents));
            }
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }

        private IEnumerable<Dependent> _dependents;
        public IEnumerable<Dependent> Dependents { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }  
    }
}
