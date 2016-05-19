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
using System.Windows.Data;

namespace ViewModel
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel()
        {            
        }

        public int SelectedIndex { get; set; }

        public string SelectedSearchType { get; set; }
        public string SelectedSearchValue { get; set; }
        

        private Spouse CustomerSpouse { get; set; }

        private Dependent newDependent;
        public Dependent Dependent
        {
            get
            {
                return newDependent;
            }
            set
            {
                newDependent = value;
                OnPropertyChanged("Dependent");
            }
        }

        public void Load()
        {
            Customers = new ObservableCollection<Customer>(context.CompleteCustomersInfo());
        }

        public void CreateEntity(object ob)
        {
            if (ob is Spouse)
            {
                CustomerSpouse = ob as Spouse;
            }           
            else if(ob is Dependent)
            {
                Dependent = ob as Dependent;
            }
        }

        private IEnumerable<Customer> customers;
        public IEnumerable<Customer> Customers
        {
            get
            {                
                return customers;
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
                OnPropertyChanged("Dependents");
            }
        }
      
        public ICollection<Dependent> Dependents
        {
            get
            {
                if(CurrentSelectedCustomer != null)
                {
                    return new ObservableCollection<Dependent>(CurrentSelectedCustomer.Dependents);
                }
                return null;                
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
            if (CustomerSpouse != null)
            {
                customer.CustomerSpouse = this.CustomerSpouse;
            }
            if (SelectedIndex == -1 && null != customer.FirstName)
            {
                context.Customers.Add(customer);
                CurrentSelectedCustomer = null;
            }
            context.SaveChanges();
            Dependent = null;
            OnPropertyChanged("Dependent");
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

        private void AddDependent()
        {
            CurrentSelectedCustomer.Dependents.Add(Dependent);
            Dependent = null;
            OnPropertyChanged("Dependents");
            OnPropertyChanged("Dependent");
        }

        private void SearchCustomer()
        {
            Customers = null;
            IQueryable<Customer> results = context.GetCustomersByParam(SelectedSearchType, SelectedSearchValue);
            if (results != null)
            {
                Customers = new ObservableCollection<Customer>(results);
            }                        
        }

        private bool canExecute = true;
        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
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

        ICommand _addDependentCommand;
        public ICommand AddDependentCommand
        {
            get
            {
                if (_addDependentCommand == null)
                {
                    _addDependentCommand = new RelayCommand(AddDependent);
                }
                return _addDependentCommand;
            }
        }
        
        ICommand _searchCustomerCommand;
        public ICommand SearchCustomerCommand
        {
            get
            {
                if (_searchCustomerCommand == null)
                {
                    _searchCustomerCommand = new RelayCommand(SearchCustomer);
                }
                return _searchCustomerCommand;
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

        public static IQueryable<Customer> GetCustomersByParam(this ManagerDBContext context, string searchType, string searchValue)
        {
            IQueryable<Customer> customers = null;
            switch (searchType)
            {
                case "Last Name":
                    customers = context.Customers
                        .Include(d => d.Dependents)
                .Include(e => e.CustomerSpouse);
                    break;
                case "First Name":
                    break;
                case "Address":
                    break;
                case "Phone Number":
                    break;
                default:
                    customers = context.Customers
                        .Include(d => d.Dependents)
                .Include(e => e.CustomerSpouse);
                    break;
            }
            return customers;
        }
    }
}

