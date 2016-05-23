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
        private Company CustomerCompany { get; set; }

        public void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(context.CompleteCustomersInfo());
        }

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
            LoadCustomers();           
        }      

        public void DeleteCustomer()
        {
            context.Customers.Remove(CurrentSelectedCustomer);
            context.SaveChanges();
            CurrentSelectedCustomer = null;            
            OnPropertyChanged("CurrentSelectedCustomer");
            LoadCustomers();
        }

        // This cancels currently selected customer to clear data in panel
        private void AddCustomer()
        {
            CurrentSelectedCustomer = null;
            Customer customer = new Customer();
            CurrentSelectedCustomer = customer;
            SelectedIndex = -1;            
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
            IQueryable<Customer> customers = context.GetCustomersByParam(SelectedSearchType, SelectedSearchValue);
            if(customers != null)
            {
                Customers = new ObservableCollection<Customer>(customers);
            }
            else
            {
                Customers = null;
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

        ICommand _deleteCustomerCommand;
        public ICommand DeleteCustomerCommand
        {
            get
            {
                if (_deleteCustomerCommand == null)
                {
                    _deleteCustomerCommand = new RelayCommand(DeleteCustomer);
                }
                return _deleteCustomerCommand;
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
                case "First/Last Name":
                    customers = context.CompleteCustomersInfo().Where(p => p.LastName.ToUpper().Contains(searchValue.Trim().ToUpper()));
                    if (customers.Count() == 0)
                    {
                        customers = context.CompleteCustomersInfo().Where(p => p.FirstName.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()));
                    }
                    break;
                case "Company Name":
                    customers = context.CompleteCustomersInfo().Where(p => p.CustomerCompany.Name.ToUpper().Contains(searchValue.Trim().ToUpper()));
                    break;
                case "Address":
                    customers = GetCustomersByAddress(context, searchValue, 0);
                    break;
                case "Search By: All":
                    customers = context.CompleteCustomersInfo();
                    break;
            }
            return customers;
        }

        private static IQueryable<Customer> GetCustomersByAddress(this ManagerDBContext context, string searchValue, int counter)
        {
            IQueryable<Customer> customers = null;
            string[] address = { "Unit Number", "Street", "VillageDistrict", "City", "State", "Country" };
            switch (address[counter])
            {
                case "Unit Number":
                    customers = context.CompleteCustomersInfo().Where(p => p.NumBuilding.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()));
                    break;
                case "Street":
                    customers = context.CompleteCustomersInfo().Where(p => p.Street.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()));
                    break;
                case "VillageDistrict":
                    customers = context.CompleteCustomersInfo().Where(p => p.VillageDistrict.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()));
                    break;
                case "City":
                    customers = context.CompleteCustomersInfo().Where(p => p.City.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()));
                    break;
                case "State":
                    customers = context.CompleteCustomersInfo().Where(p => p.State.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()));
                    break;
                case "Country":
                    customers = context.CompleteCustomersInfo().Where(p => p.Country.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()));
                    break;
            }
            if (customers.Count() == 0 && counter < address.Length-1)
            {                
                customers = GetCustomersByAddress(context, searchValue, counter+1);
            }
            return customers;
        }
    }
}

