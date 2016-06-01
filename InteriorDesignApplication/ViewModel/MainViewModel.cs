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
using Model.Helpers;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace ViewModel
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel()
        {
        }


        public void InitializeAndLoadEntities()
        {
            LoadEntities();
            InitializeEntities();
        }


        private void LoadEntities()
        {            
            LoadCustomers();
            LoadUtilityBillTypes();            
        }

        private void InitializeEntities()
        {
            CurrentSelectedCustomer = null;
            Utilities = null;
            SelectedIndex = -1;
        }

        private void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(context.CompleteCustomersInfo());           
        }

        private void LoadUtilityBillTypes()
        {
            UtilityBillTypes = new ObservableCollection<UtilityBillType>(context.GetAllUtilityBillTypes());
        }

        public void CreateEntity(object ob)
        {
            if (ob is Spouse)
            {
                CustomerSpouse = ob as Spouse;
            }
            else if (ob is Dependent)
            {
                Dependent = ob as Dependent;
            }
            else if (ob is Utility)
            {
                Utility = ob as Utility;
            }            
            else if (ob is UtilityBillType)
            {
                UtilityBillType = ob as UtilityBillType;
            }
            else if (ob is UtilityCompany)
            {
                UtilityCompany = ob as UtilityCompany;
            }
            else if (ob is Bank)
            {
                CustomerBank = ob as Bank;
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

        #region Personal Information
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
                if (currentSelectedCustomer != null)
                {
                    Utilities = CurrentSelectedCustomer.Utilities;
                    if (currentSelectedCustomer.CustomerCompany == null)
                    {
                        currentSelectedCustomer.CustomerCompany = new Company();
                    }
                    if (currentSelectedCustomer.ImageSourceLocation != null)
                    {
                        CustomerImageSource = new BitmapImage(new Uri(currentSelectedCustomer.ImageSourceLocation));
                    }
                    else
                    {
                        CustomerImageSource = null;
                    }
                    
                }                
                OnPropertyChanged("CurrentSelectedCustomer");
                OnPropertyChanged("Dependents");
                OnPropertyChanged("Utilities");
                OnPropertyChanged("Banks");                
            }
        }

        private Dependent currentSelectedDependent;
        public Dependent CurrentSelectedDependent
        {
            get
            {
                return currentSelectedDependent;
            }
            set
            {
                currentSelectedDependent = value;
                OnPropertyChanged("CurrentSelectedDependent");
            }
        }
       
        public ICollection<Dependent> Dependents
        {
            get
            {
                if (CurrentSelectedCustomer != null)
                {
                    return new ObservableCollection<Dependent>(CurrentSelectedCustomer.Dependents);
                }
                return null;
            }
        }

        private ImageSource customerImageSource;
        public ImageSource CustomerImageSource
        {
            get
            {
                return customerImageSource;
            }
            set
            {
                customerImageSource = value;
                OnPropertyChanged("CustomerImageSource");
            }
        }

        private Bank newBank;
        public Bank CustomerBank
        {
            get
            {
                return newBank;
            }
            set
            {
                newBank = value;
                OnPropertyChanged("Bank");
            }
        }

        private Bank currentSelectedBank;
        public Bank CurrentSelectedBank
        {
            get
            {
                return currentSelectedBank;
            }
            set
            {
                currentSelectedBank = value;
                OnPropertyChanged("CurrentSelectedBank");
            }
        }

        public ICollection<Bank> Banks
        {
            get
            {
                if (CurrentSelectedCustomer != null)
                {
                    return new ObservableCollection<Bank>(CurrentSelectedCustomer.Banks);
                }
                return null;
            }
        }

        #endregion

        #region Utilities
        private Utility newUtility;
        public Utility Utility
        {
            get
            {
                return newUtility;
            }
            set
            {
                newUtility = value;
                OnPropertyChanged("Utility");
            }
        }

        private Utility currentSelectedUtility;
        public Utility CurrentSelectedUtility
        {
            get
            {
                return currentSelectedUtility;
            }
            set
            {
                currentSelectedUtility = value;
                OnPropertyChanged("CurrentSelectedUtility");
            }
        }       

        private IEnumerable<Utility> utilities;
        public IEnumerable<Utility> Utilities
        {
            get
            {                
                return utilities;
            }
            set
            {
                utilities = value;
                OnPropertyChanged("Utilities");
            }
        }

        private UtilityBillType currentSelectedUtilityBillType;
        public UtilityBillType CurrentSelectedUtilityBillType
        {
            get
            {
                return currentSelectedUtilityBillType;
            }
            set
            {
                currentSelectedUtilityBillType = value;
                OnPropertyChanged("CurrentSelectedUtilityBillType");
                OnPropertyChanged("UtilityCompanies");
            }
        }

        private UtilityBillType newUtilityBillType;
        public UtilityBillType UtilityBillType
        {
            get
            {
                return newUtilityBillType;
            }
            set
            {
                newUtilityBillType = value;
                OnPropertyChanged("UtilityBillType");
            }
        }

        private ICollection<UtilityBillType> utilityBillTypes;
        public ICollection<UtilityBillType> UtilityBillTypes
        {
            get
            {
                return utilityBillTypes;
            }
            set
            {
                utilityBillTypes = value;
                OnPropertyChanged("UtilityBillTypes");
            }
        }

        public ICollection<UtilityCompany> UtilityCompanies
        {
            get
            {
                if (CurrentSelectedUtilityBillType != null)
                {
                    return new ObservableCollection<UtilityCompany>(CurrentSelectedUtilityBillType.UtilityCompanies);
                }
                return null;
            }
        }

        private UtilityCompany currentSelectedUtilityCompany;
        public UtilityCompany CurrentSelectedUtilityCompany
        {
            get
            {
                return currentSelectedUtilityCompany;
            }
            set
            {
                currentSelectedUtilityCompany = value;
                OnPropertyChanged("CurrentSelectedUtilityCompany");
            }
        }

        private UtilityCompany newUtilityCompany;
        public UtilityCompany UtilityCompany
        {
            get
            {
                return newUtilityCompany;
            }
            set
            {
                newUtilityCompany = value;
                OnPropertyChanged("UtilityCompany");
            }
        }

        private DateTime? utilityCutOffDate;
        public DateTime? UtilityCutoffDate
        {
            get
            {
                return utilityCutOffDate;
            }
            set
            {
                utilityCutOffDate = value;
                OnPropertyChanged("Utility");
                OnPropertyChanged("UtilityCutoffDate");
                OnPropertyChanged("UtilityStatus");
            }
        }

        private string utilityReceipt;
        public string UtilityReceipt
        {
            get
            {
                return utilityReceipt;
            }
            set
            {
                utilityReceipt = value;
                OnPropertyChanged("Utility");
                OnPropertyChanged("UtilityReceipt");
                OnPropertyChanged("UtilityStatus");
            }
        }

        public string UtilityStatus
        {
            get
            {
                return UtilityHelper.GetUtilityStatusText(UtilityCutoffDate, !String.IsNullOrEmpty(UtilityReceipt));
            }
        }
        #endregion
       

        #region Actions
        private void SaveCustomer()
        {
            try
            {
                Customer customer = CurrentSelectedCustomer as Customer;               
                customer.CustomerSpouse = this.CustomerSpouse;
                // avoid saving null customer company in db; company name required
                if (customer.CustomerCompany != null && String.IsNullOrEmpty(customer.CustomerCompany.Name))
                {
                    customer.CustomerCompany = null;
                }
                if (CustomerImageSource != null)
                {
                    customer.ImageSourceLocation = CustomerImageSource.ToString();
                }
                if (SelectedIndex == -1 && null != customer.FirstName)
                {
                    context.Customers.Add(customer);                   
                }

                context.SaveChanges();
                Dependent = null;
                CustomerBank = null;
                CustomerSpouse = null;
                LoadEntities();
            }
            catch (DbUpdateConcurrencyException ex)
            {                
                ex.Entries.Single().Reload();
            }
        }

        public void DeleteCustomer()
        {
            context.Customers.Remove(CurrentSelectedCustomer);
            context.SaveChanges();
            CurrentSelectedCustomer = null;
            LoadCustomers();
        }

        // This cancels currently selected customer to clear data in panel
        private void AddCustomer()
        {
            CurrentSelectedCustomer = null;
            Customer customer = new Customer();
            customer.CustomerCompany = new Company();
            CurrentSelectedCustomer = customer;
            SelectedIndex = -1;
        }

        private void AddBank()
        {
            CurrentSelectedCustomer.Banks.Add(CustomerBank);
            CustomerBank = null;
            OnPropertyChanged("Banks");                      
        }

        private void AddDependent()
        {
            CurrentSelectedCustomer.Dependents.Add(Dependent);
            Dependent = null;
            OnPropertyChanged("Dependents");
        }

        private void DeleteDependent()
        {
            CurrentSelectedCustomer.Dependents.Remove(CurrentSelectedDependent);
            context.Entry(CurrentSelectedDependent).State = EntityState.Deleted;
            CurrentSelectedDependent = null;
            OnPropertyChanged("Dependents");
        }

        private void SearchCustomer()
        {
            IQueryable<Customer> customers = context.GetCustomersByParam(SelectedSearchType, SelectedSearchValue);
            if (customers != null)
            {
                Customers = new ObservableCollection<Customer>(customers);
            }
            else
            {
                Customers = null;
            }
        }

        private void CreateUtility()
        {
            CurrentSelectedUtility = null;
            Utility utility = new Utility();
            UtilityReceipt = null;
            UtilityCutoffDate = null;
            CurrentSelectedUtility = utility;            
        }

        private void AddUtility()
        {
            if (UtilityBillType != null && !UtilityBillType.Name.Trim().Equals(string.Empty))
            {
                if (!UtilityBillTypes.Contains(UtilityBillType))
                {
                    UtilityBillTypes.Add(UtilityBillType);
                    CurrentSelectedUtilityBillType = UtilityBillType;
                }
                UtilityBillType = null;
                OnPropertyChanged("UtilityBillTypes");
            }
            else if ((UtilityCompany != null && !UtilityCompany.Name.Trim().Equals(string.Empty))
                && CurrentSelectedUtilityBillType != null)
            {
                if (!CurrentSelectedUtilityBillType.UtilityCompanies.Contains(UtilityCompany))
                {
                    CurrentSelectedUtilityBillType.UtilityCompanies.Add(UtilityCompany);
                    CurrentSelectedUtilityCompany = UtilityCompany;
                }
                UtilityCompany = null;
                OnPropertyChanged("UtilityCompanies");
            }
            else if (CurrentSelectedUtilityBillType != null && CurrentSelectedUtilityCompany != null)
            {
                CurrentSelectedUtility.BillType = CurrentSelectedUtilityBillType;
                CurrentSelectedUtility.UtilityCompany = CurrentSelectedUtilityCompany;
                CurrentSelectedUtility.Receipt = UtilityReceipt;
                CurrentSelectedUtility.CutoffDate = UtilityCutoffDate;
                CurrentSelectedCustomer.Utilities.Add(CurrentSelectedUtility);
                Utilities = new ObservableCollection<Utility>(CurrentSelectedCustomer.Utilities);                

                // set properties to null
                Utility = null;
                currentSelectedUtilityBillType = null;
                CurrentSelectedUtilityCompany = null;
                CurrentSelectedUtility = null;
                UtilityCutoffDate = null;
                UtilityReceipt = null;
            }
        }

        private void EditUpdateUtility()
        {
            if (CurrentSelectedUtility != null)
            {
                CurrentSelectedUtility.BillType = CurrentSelectedUtilityBillType;
                CurrentSelectedUtility.UtilityCompany = CurrentSelectedUtilityCompany;
                CurrentSelectedUtility.Receipt = UtilityReceipt;
                CurrentSelectedUtility.CutoffDate = UtilityCutoffDate;
                context.Entry(CurrentSelectedUtility).State = EntityState.Modified;
                Utilities = new ObservableCollection<Utility>(CurrentSelectedCustomer.Utilities);
            }
        }

        private void EditUtility()
        {
            if (CurrentSelectedUtility != null)
            {
                CurrentSelectedUtilityBillType = CurrentSelectedUtility.BillType;
                CurrentSelectedUtilityCompany = CurrentSelectedUtility.UtilityCompany;
                UtilityCutoffDate = CurrentSelectedUtility.CutoffDate;
                UtilityReceipt = CurrentSelectedUtility.Receipt;
            }            
        }

        private void DeleteUtility()
        {
            CurrentSelectedCustomer.Utilities.Remove(CurrentSelectedUtility);
            context.Entry(CurrentSelectedUtility).State = EntityState.Deleted;
            Utilities = new ObservableCollection<Utility>(CurrentSelectedCustomer.Utilities);
            CurrentSelectedUtility = null;            
        }

        private void ShowUtilityAlerts()
        {          
            IEnumerable<Utility> utilities = context.GetUtilityWithAlerts(CurrentSelectedCustomer);
            if (utilities != null)
            {
                Utilities = new ObservableCollection<Utility>(utilities);
            }
            else
            {
                Utilities = null;
            }           
        }

        private void CloseUtility()
        {
            CurrentSelectedUtility = null;
            UtilityCutoffDate = null;
            UtilityReceipt = null;
        }

        private void DeleteBank()
        {
            CurrentSelectedCustomer.Banks.Remove(CurrentSelectedBank);
            context.Entry(CurrentSelectedBank).State = EntityState.Deleted;
            CurrentSelectedBank = null;
            OnPropertyChanged("Banks");
        }
        #endregion

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
                if (_addCustomerCommand == null)
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

        ICommand _addBankCommand;
        public ICommand AddBankCommand
        {
            get
            {
                if (_addBankCommand == null)
                {
                    _addBankCommand = new RelayCommand(AddBank);
                }
                return _addBankCommand;
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

        ICommand _deleteDependentCommand;
        public ICommand DeleteDependentCommand
        {
            get
            {
                if (_deleteDependentCommand == null)
                {
                    _deleteDependentCommand = new RelayCommand(DeleteDependent);
                }
                return _deleteDependentCommand;
            }
        }

        ICommand _addUtilityCommand;
        public ICommand AddUtilityCommand
        {
            get
            {
                if (_addUtilityCommand == null)
                {
                    _addUtilityCommand = new RelayCommand(AddUtility);
                }
                return _addUtilityCommand;
            }
        }

        ICommand _editUpdateUtilityCommand;
        public ICommand EditUpdateUtilityCommand
        {
            get
            {
                if (_editUpdateUtilityCommand == null)
                {
                    _editUpdateUtilityCommand = new RelayCommand(EditUpdateUtility);
                }
                return _editUpdateUtilityCommand;
            }
        }

        ICommand _deleteUtilityCommand;
        public ICommand DeleteUtilityCommand
        {
            get
            {
                if (_deleteUtilityCommand == null)
                {
                    _deleteUtilityCommand = new RelayCommand(DeleteUtility);
                }
                return _deleteUtilityCommand;
            }
        }
        
        ICommand _createUtilityCommand;
        public ICommand CreateUtilityCommand
        {
            get
            {
                if (_createUtilityCommand == null)
                {
                    _createUtilityCommand = new RelayCommand(CreateUtility);
                }
                return _createUtilityCommand;
            }
        }

        ICommand _editUtilityCommand;
        public ICommand EditUtilityCommand
        {
            get
            {
                if (_editUtilityCommand == null)
                {
                    _editUtilityCommand = new RelayCommand(EditUtility);
                }
                return _editUtilityCommand;
            }
        }

        ICommand _closeUtilityCommand;
        public ICommand CloseUtilityCommand
        {
            get
            {
                if (_closeUtilityCommand == null)
                {
                    _closeUtilityCommand = new RelayCommand(CloseUtility);
                }
                return _closeUtilityCommand;
            }
        }




        ICommand _showUtilityAlertsCommand;
        public ICommand ShowUtilityAlertsCommand
        {
            get
            {
                if (_showUtilityAlertsCommand == null)
                {
                    _showUtilityAlertsCommand = new RelayCommand(ShowUtilityAlerts);
                }
                return _showUtilityAlertsCommand;
            }
        }

        ICommand _deleteBankCommand;
        public ICommand DeleteBankCommand
        {
            get
            {
                if (_deleteBankCommand == null)
                {
                    _deleteBankCommand = new RelayCommand(DeleteBank);
                }
                return _deleteBankCommand;
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

    #region RelayCommand
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
    #endregion

    #region DBContext Queries
    public static class Extensions
    {
        public static IQueryable<Customer> CompleteCustomersInfo(this ManagerDBContext context)
        {
            return context.Customers
                .Include(d => d.Dependents)
                .Include(e => e.Banks)
                .Include(f => f.CustomerSpouse)
                .Include(g => g.CustomerCompany)
                .Include(h => h.Utilities);
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

        public static IQueryable<UtilityBillType> GetAllUtilityBillTypes(this ManagerDBContext context)
        {
            return context.UtilityBillTypes
               .Include(d => d.UtilityCompanies);
        }

        public static IEnumerable<Utility> GetUtilityWithAlerts(this ManagerDBContext context, Customer currentSelectedCustomer)
        {
            return context.Utilities.ToList()
                .Where(u => u.CustomerId == currentSelectedCustomer.Id)
                .Where(u => !u.Status.Contains("Paid"))
                .Where(u => u.DaysDue <= 5);
        }
    }
    #endregion

}

