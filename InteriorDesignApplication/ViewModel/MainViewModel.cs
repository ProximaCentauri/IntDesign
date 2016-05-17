using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;
using System.Collections.ObjectModel;
using System.Data.Entity;
using Model.Controls;

namespace ViewModel
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel()
        {

        }

        public int SelectedIndex { get; set; }

        private IEnumerable<Customer> customers;
        public IEnumerable<Customer> Customers
        {
            get
            {
                return new ObservableCollection<Customer>(context.CompleteCustomersInfo());
            }
            set
            {
                customers = value;
                OnPropertyChanged("Customers");
            }
        }

        private Customer currentSelectedCustomer;
        public Customer CurrentSelectedCustomer
        {
            get
            {
                return currentSelectedCustomer;
            }
            set
            {
                currentSelectedCustomer = value;
                OnPropertyChanged("CurrentSelectedCustomer");
            }
        }

        private IEnumerable<Dependent> dependents;
        public IEnumerable<Dependent> Dependents
        {
            get
            {
                return new ObservableCollection<Dependent>(context.Dependents);
            }
            set
            {
                dependents = value;
                OnPropertyChanged("Dependents");
            }
        }

        private PopupView currentPopupView;
        public PopupView CurrentPopupView
        {
            get
            {
                return currentPopupView;
            }
            set
            {
                currentPopupView = value;
                OnPropertyChanged("CurrentPopupView");
            }
        }

        private void SaveCustomer()
        {
            Customer customer = CurrentSelectedCustomer as Customer;
            if (SelectedIndex == -1)
            {
                context.Customers.Add(customer);
                CurrentSelectedCustomer = null;
            }
            context.SaveChanges();
            OnPropertyChanged("Customers");                       
        }      

        public void DeleteCustomer(Customer customer)
        {
            var customerToDelete = context.Customers.SingleOrDefault(x => x.Id == customer.Id);
            context.Customers.Remove(customerToDelete);
            context.SaveChanges();
        }

        // This cancels currently selected customer to clear data in panel
        private void AddCustomer()
        {
            CurrentSelectedCustomer = null;
            Customer customer = new Customer();
            CurrentSelectedCustomer = customer;
        }

        private void OpenPopupView()
        {
            OnPropertyChanged("PopupView");
        }

        #region INotifyPropertyChanged Implementing
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Action Commands
        public ICommand ActionCommand { get; set; }

        ICommand _addCustomerCommand;
        public ICommand AddCustomerCommand
        {
            get
            {
                if(_addCustomerCommand == null)
                {
                    _addCustomerCommand = new RelayCommand(AddCustomer);                
                }
                return _addCustomerCommand;
            }
        }

        ICommand _saveCustomerCommand;
        public ICommand SaveCustomerCommand
        {
            get
            {
                if (_saveCustomerCommand == null)
                {
                    _saveCustomerCommand = new RelayCommand(SaveCustomer);
                }
                return _saveCustomerCommand;
            }
        }

        ICommand openPopupViewCommand;
        public ICommand OpenPopupViewCommand
        {
            get
            {
                if (null == openPopupViewCommand)
                {
                    
                    openPopupViewCommand = new RelayCommand(OpenPopupView);
                }
                return openPopupViewCommand;
            }
        }

        #endregion

        public void Dispose()
        {
            if (!dispose)
                dispose = true;
        }

        private bool dispose;
        private readonly ManagerDBContext context = new ManagerDBContext();
    }

    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private Action methodToExecute;
        private Func<bool> canExecuteEvaluator;
        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }
        public RelayCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }
        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }
        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke();
        }
    }

    public static class Extensions
    {
        public static IQueryable<Customer> CompleteCustomersInfo(this ManagerDBContext context)
        {
            return context.Customers
                .Include(d => d.Dependents)
                .Include(e => e.CustomerSpouse);
        }      
    }
}

