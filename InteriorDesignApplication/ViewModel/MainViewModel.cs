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

namespace ViewModel
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel()
        {
            
        }

        private IEnumerable<Customer> customers;
        public IEnumerable<Customer> Customers
        {
            get
            {
                return new ObservableCollection<Customer>(context.Customers.Include(d => d.Dependents));              
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

        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            OnPropertyChanged("Customers");
        }

        public void EditCustomer(Customer customer)
        {
            Customer customerToUpdate = context.Customers.SingleOrDefault(x => x.Id == customer.Id);
            if (null != customerToUpdate)
            {
                customerToUpdate.FirstName = customer.FirstName;
                customerToUpdate.MiddleName = customer.MiddleName;
                customerToUpdate.LastName = customer.LastName;
            }
            context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            var customerToDelete = context.Customers.SingleOrDefault(x => x.Id == customer.Id);
            context.Customers.Remove(customerToDelete);
            context.SaveChanges();
        }

        #region INotifyPropertyChanged Implementing
        public event PropertyChangedEventHandler PropertyChanged;
                
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public ICommand ActionCommand { get; set; }

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
}

