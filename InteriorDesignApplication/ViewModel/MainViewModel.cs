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
using log4net;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;


namespace ViewModel
{
    public class MainViewModel : IMainViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            LoadBankTypes();
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

        private void LoadBankTypes()
        {
            BankTypes = new ObservableCollection<BankType>(context.GetAllBankTypes());
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

        private bool readyToSave;
        public bool ReadyToSave
        {
            get { return readyToSave; }
            set { readyToSave = value; }
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }

        #region Personal Information
        public int SelectedIndex { get; set; }
        public string CommandParameter { get; set; }
        public string SelectedSearchType { get; set; }
        public string SelectedSearchValue { get; set; }
        public string Username { get; set; }
        public string EmailAd { get; set; }
        public string TemporaryPIN { get; set; }

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
                    // discard un-saved changes from db context
                    if (SelectedIndex != -1)
                    {
                        ResetContext();
                    }

                    Utilities = CurrentSelectedCustomer.Utilities;
                    if (currentSelectedCustomer.CustomerCompany == null)
                    {
                        currentSelectedCustomer.CustomerCompany = new Company();
                    }

                    if (currentSelectedCustomer.ImageSourceLocation != null)
                    {
                        string file = new Uri(currentSelectedCustomer.ImageSourceLocation).LocalPath;

                        if (File.Exists(file))
                            CustomerImageSource = new BitmapImage(new Uri(file));
                        else
                            OnPropertyChanged("FileNotFound");
                    }
                    else
                    {
                        // this stays in here! do not move somewhere else!
                        CustomerImageSource = null;
                    }

                    if(currentSelectedCustomer.TitleInfo == null)
                    {
                        currentSelectedCustomer.TitleInfo = new Title();                      
                    }                    
                    UnitTotalCost = currentSelectedCustomer.TitleInfo.UnitCost;              

                    if(currentSelectedCustomer.FitOut == null)
                    {
                        currentSelectedCustomer.FitOut = new FitOut();
                    }
                    FitOutCompletionDate = currentSelectedCustomer.FitOut.EndDate;
                }                
                OnPropertyChanged("CurrentSelectedCustomer");
                OnPropertyChanged("Dependents");
                OnPropertyChanged("Utilities");
                OnPropertyChanged("Banks");
                OnPropertyChanged("Appliances");
                OnPropertyChanged("Payments");
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

        private BankType currentSelectedBankType;
        public BankType CurrentSelectedBankType
        {
            get
            {
                return currentSelectedBankType;
            }
            set
            {
                currentSelectedBankType = value;
                OnPropertyChanged("CurrentSelectedBankType");                
            }
        }


        private ICollection<BankType> bankTypes;
        public ICollection<BankType> BankTypes
        {
            get
            {
                return bankTypes;
            }
            set
            {
                bankTypes = value;
                OnPropertyChanged("BankTypes");
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

        
        private DateTime? utilityCutOffDate;
        public DateTime? UtilityCutoffDate
        {
            get
            {
                if (utilityCutOffDate == null)
                {
                    utilityCutOffDate = DateTime.Now;
                }
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

        private double unitTotalCost;
        public double UnitTotalCost
        {
            get
            {
                return unitTotalCost;
            }
            set
            {
                unitTotalCost = value;
                if(CurrentSelectedCustomer != null && CurrentSelectedCustomer.TitleInfo != null)
                {
                    CurrentSelectedCustomer.TitleInfo.UnitCost = value;
                }
                OnPropertyChanged("UnitTotalCost");
                OnPropertyChanged("UnitTotalPayment");
                OnPropertyChanged("UnitRemainingBalance");
            }
        }
        
        public double UnitTotalPayment
        {
            get
            {
                double total = 0.0;
                if(CurrentSelectedCustomer != null && CurrentSelectedCustomer.TitleInfo != null)
                {
                    total = CurrentSelectedCustomer.TitleInfo.Payments.Sum(j => j.Amount);
                    CurrentSelectedCustomer.TitleInfo.TotalPayment = total;
                }
                return total;
            }
        }
       
        public double UnitRemainingBalance
        {
            get
            {
                return UnitTotalCost - UnitTotalPayment;               
            }           
        }

       
        public IEnumerable<Payment> Payments
        {
            get
            {
                if (CurrentSelectedCustomer != null)
                {
                    return new ObservableCollection<Payment>(CurrentSelectedCustomer.TitleInfo.Payments);
                }
                return null;
            }
        }

        private Payment currentSelectedPayment;
        public Payment CurrentSelectedPayment
        {
            get
            {
                return currentSelectedPayment;
            }
            set
            {
                currentSelectedPayment = value;
                OnPropertyChanged("CurrentSelectedPayment");                
            }
        }
               
        #endregion
       

        #region Actions
        private void SaveCustomer()
        {
            try
            {
                OnPropertyChanged("SavingCustomer");
                if (readyToSave)
                {
                    OnPropertyChanged("ReadyToSave");
                    Customer customer = CurrentSelectedCustomer as Customer;
                    Log.InfoFormat("Saving customer:{0} {1}...", customer.FirstName, customer.LastName);
                    customer.CustomerSpouse = this.CustomerSpouse;                    
                    if (CustomerImageSource != null)
                    {
                        customer.ImageSourceLocation = CustomerImageSource.ToString();
                    }
                    if (SelectedIndex == -1 && null != customer.FirstName)
                    {
                        context.Customers.Add(customer);
                    }
                    customer.FitOut.EndDate = FitOutCompletionDate;
                    context.SaveChanges();
                    Dependent = null;
                    CustomerSpouse = null;
                    LoadEntities();
                    OnPropertyChanged("SavedCustomer");
                    Log.Info("Successfully save the customer");
                }
                else
                {
                    OnPropertyChanged("EmptyFields");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {                       
                ex.Entries.Single().Reload();
                context.SaveChanges();
                OnPropertyChanged("SavedCustomer");
            }
            if (!currentAppUser.IsAdmin)
                OnPropertyChanged("NonAdmin");
        }
       
        public void DeleteCustomer()
        {
            Log.InfoFormat("Deleting customer:{0} {1}", CurrentSelectedCustomer.FirstName, CurrentSelectedCustomer.LastName);
            context.Customers.Remove(CurrentSelectedCustomer);
            context.SaveChanges();
            CurrentSelectedCustomer = null;
            LoadCustomers();
        }

        // This cancels currently selected customer to clear data in panel
        private void AddCustomer()
        {
            SelectedIndex = -1; 
            OnPropertyChanged("AddCustomer");
            CurrentSelectedCustomer = null;            
            CurrentSelectedCustomer = new Customer();
                       
        }

        private void AddDependent()
        {
            if (readyToSave)
            {
                CurrentSelectedCustomer.Dependents.Add(Dependent);
                Dependent = null;
                OnPropertyChanged("Dependents");
            }
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

        private void SearchUser()
        {
            CurrentAppUser = context.GetUserByParam(Username, DataEncryptor.Encrypt(CommandParameter));
            if (currentAppUser != null)
            {
                OnPropertyChanged("LoginSuccessful");

                if (!currentAppUser.IsAdmin)
                    OnPropertyChanged("NonAdmin");
            }
            else
            {
                OnPropertyChanged("InvalidUser");
            }                
        }

        private void LogoutUser()
        {
            if (CurrentAppUser != null)
            {
                CurrentAppUser = null;
                Username = string.Empty;
                CommandParameter = string.Empty;
                OnPropertyChanged("LogoutUser");
            }            
        }

        private void ChangeUserPassword()
        {
            if (CurrentAppUser != null)
            {
                CurrentAppUser.CurrentPassword = DataEncryptor.Encrypt(CommandParameter);
                bool success = SaveContext(CurrentAppUser);
                CommandParameter = string.Empty;
                if (success)
                {
                    OnPropertyChanged("PasswordChangeSuccessful");
                    OnPropertyChanged("PasswordChanged");
                }
            }
        }

        private bool SaveContext(object entity)
        {
            bool success = false;
            using (var newContext = new ManagerDBContext())
            {
                newContext.Entry(entity).State = EntityState.Modified;                
                try
                {
                    newContext.SaveChanges();                
                    success = true;
                }
                catch (Exception ex)
                {
                    Log.ErrorFormat("Exception while saving context: {0}", ex.ToString());
                }                
            }
            return success;
        }

        private void SendTemporaryPIN()
        {
            if (!string.IsNullOrEmpty(EmailAd) && EmailManager.IsValidEmail(EmailAd))
            {
                Random r = new Random();
                string tp = r.Next(0, 1000000).ToString("D6");
                bool success = false;
                CurrentAppUser = context.GetUserByEmail(EmailAd);
                if (CurrentAppUser != null)
                {
                    if (EmailManager.Send(EmailAd, "", "Temporary PIN", "Your temporary pin: " + tp))
                    {
                        OnPropertyChanged("TemporaryPINSent");
                        CurrentAppUser.TemporaryPin = tp;
                        success = SaveContext(CurrentAppUser);
                    }
                    else
                    {
                        OnPropertyChanged("TemporaryPINSendFailed");
                    }
                    if (success)
                    {
                        OnPropertyChanged("TemporaryPINAlreadySent");
                    }
                }
                else
                {
                    OnPropertyChanged("InvalidEmailAdd");
                }
            }
            else
            {
                OnPropertyChanged("InvalidEmailAdd");
            }            
        }

        private void VerifyResetPassword()
        {
            if (!string.IsNullOrEmpty(EmailAd) && !string.IsNullOrEmpty(TemporaryPIN) && EmailManager.IsValidEmail(EmailAd))
            {
                var currentUser = context.GetUserByEmailandPIN(EmailAd, TemporaryPIN);
                if (currentUser != null)
                {
                    CurrentAppUser = currentUser;
                    OnPropertyChanged("ValidEmailAdd");
                }
                else
                {
                    OnPropertyChanged("InvalidEmailAdd");
                }
            }
            else
            {
                OnPropertyChanged("InvalidEmailAdd");
            }
        }

        private void ResetPassword()
        {
            if (CurrentAppUser != null)
            {
                CurrentAppUser.CurrentPassword = DataEncryptor.Encrypt(CommandParameter);
                bool success = SaveContext(CurrentAppUser);
                CommandParameter = string.Empty;
                if (success)
                {
                    OnPropertyChanged("PasswordResetSuccessful");
                    OnPropertyChanged("LoginSuccessful");
                    //delete the temporary PIN
                    CurrentAppUser.TemporaryPin = string.Empty;
                    SaveContext(CurrentAppUser);
                }
            }
        }

        private void AddBankType()
        {
            if(CurrentSelectedBankType != null && !CurrentSelectedBankType.Name.Trim().Equals(string.Empty))
            {
                if (!BankTypes.Contains(CurrentSelectedBankType))
                {
                    BankTypes.Add(CurrentSelectedBankType);
                }
                OnPropertyChanged("CurrentSelectedBankType");
                OnPropertyChanged("BankTypes");
            }            
        }        

        private void CreateBank()
        {
            CurrentSelectedBank = null;
            CurrentSelectedBankType = new BankType();
            CurrentSelectedBank = new Bank();
        }

        private void CreateBankType()
        {            
            CurrentSelectedBankType = new BankType();            
        }

        private void AddBank()
        {
            if (CurrentSelectedBankType != null)
            {
                CurrentSelectedBank.BankType = CurrentSelectedBankType;
                CurrentSelectedCustomer.Banks.Add(CurrentSelectedBank);
                context.Entry(CurrentSelectedBank).State = EntityState.Added;
                CurrentSelectedBank = null;
                CurrentSelectedBankType = null;
                OnPropertyChanged("Banks");
            }  
        }

        private void EditBank()
        {
            if (CurrentSelectedBank != null)
            {
                CurrentSelectedBankType = CurrentSelectedBank.BankType;                
            }
        }

        private void EditUpdateBank()
        {
            if (CurrentSelectedBank != null)
            {
                CurrentSelectedBank.BankType = CurrentSelectedBankType;
                context.Entry(CurrentSelectedBank).State = EntityState.Modified;
                CurrentSelectedBank = null;
                currentSelectedBankType = null;
                OnPropertyChanged("Banks");

            }
        }

        private void DeleteBank()
        {
            CurrentSelectedCustomer.Banks.Remove(CurrentSelectedBank);
            context.Entry(CurrentSelectedBank).State = EntityState.Deleted;
            CurrentSelectedBank = null;            
            OnPropertyChanged("Banks");
        }

        
        private void CreateUtility()
        {
            CurrentSelectedUtility = null;            
            UtilityReceipt = null;
            UtilityCutoffDate = null;
            CurrentSelectedUtilityBillType = new UtilityBillType();
            CurrentSelectedUtilityCompany = new UtilityCompany();
            CurrentSelectedUtility = new Utility();            
        }

        private void CreateUtilityBillType()
        {
            CurrentSelectedUtilityBillType = new UtilityBillType();
        }

        private void CreateUtilityCompany()
        {
            CurrentSelectedUtilityCompany = new UtilityCompany();
        }

        private void AddUtility()
        {
            if (CurrentSelectedUtilityBillType != null && CurrentSelectedUtilityCompany != null)
            {
                CurrentSelectedUtility.BillType = CurrentSelectedUtilityBillType;
                CurrentSelectedUtility.UtilityCompany = CurrentSelectedUtilityCompany;
                CurrentSelectedUtility.Receipt = UtilityReceipt;
                CurrentSelectedUtility.CutoffDate = UtilityCutoffDate;
                CurrentSelectedCustomer.Utilities.Add(CurrentSelectedUtility);
                context.Entry(CurrentSelectedUtility).State = EntityState.Added;
                Utilities = new ObservableCollection<Utility>(CurrentSelectedCustomer.Utilities);
                // set properties to null
                Utility = null;
                CurrentSelectedUtilityBillType = null;
                CurrentSelectedUtilityCompany = null;
                CurrentSelectedUtility = null;
                UtilityCutoffDate = null;
                UtilityReceipt = null;
            }
        }

        private void AddUtilityBillType()
        {
            if (CurrentSelectedUtilityBillType != null && !CurrentSelectedUtilityBillType.Name.Trim().Equals(string.Empty))
            {
                if (!UtilityBillTypes.Contains(CurrentSelectedUtilityBillType))
                {
                    UtilityBillTypes.Add(CurrentSelectedUtilityBillType);
                }
                OnPropertyChanged("CurrentSelectedUtilityBillType");
                OnPropertyChanged("UtilityBillTypes");
            }
        }

        private void AddUtilityCompany()
        {
            if ((CurrentSelectedUtilityCompany != null && !CurrentSelectedUtilityCompany.Name.Trim().Equals(string.Empty))
                && CurrentSelectedUtilityBillType != null)
            {
                if (!CurrentSelectedUtilityBillType.UtilityCompanies.Contains(CurrentSelectedUtilityCompany))
                {
                    CurrentSelectedUtilityBillType.UtilityCompanies.Add(CurrentSelectedUtilityCompany);
                }
                OnPropertyChanged("CurrentSelectedUtilityCompany");
                OnPropertyChanged("UtilityCompanies");
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

        private void CancelChanges()
        {
            ResetContext();            
            CurrentSelectedCustomer = (Customer)context.Entry(CurrentSelectedCustomer).Entity;
            OnPropertyChanged("CurrentSelectedCustomer");            
        }

        private void ResetContext()
        {
            foreach (DbEntityEntry entry in context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default:
                        break;
                }
            }
        }

        private void CreatePayment()
        {
            CurrentSelectedPayment = null;            
            CurrentSelectedPayment = new Payment();
        }

        private void AddPayment()
        {
            CurrentSelectedCustomer.TitleInfo.Payments.Add(CurrentSelectedPayment);
            context.Entry(CurrentSelectedPayment).State = EntityState.Added;            
            CurrentSelectedPayment = null;
            OnPropertyChanged("Payments");
            OnPropertyChanged("UnitTotalPayment");
            OnPropertyChanged("UnitRemainingBalance");
        }

        private void DeletePayment()
        {
            CurrentSelectedCustomer.TitleInfo.Payments.Remove(CurrentSelectedPayment);
            context.Entry(CurrentSelectedPayment).State = EntityState.Deleted;
            CurrentSelectedPayment = null;
            OnPropertyChanged("Payments");
            OnPropertyChanged("UnitTotalPayment");
            OnPropertyChanged("UnitRemainingBalance");
        }

        private void EditUpdatePayment()
        {
            if (CurrentSelectedPayment != null)
            {
                context.Entry(CurrentSelectedPayment).State = EntityState.Modified;                
                OnPropertyChanged("Payments");
                OnPropertyChanged("UnitTotalPayment");
                OnPropertyChanged("UnitRemainingBalance");
            }
        }

        #endregion

        #region Appliance
        private Appliance currentSelectedAppliance;
        public Appliance CurrentSelectedAppliance
        {
            get
            {
                return currentSelectedAppliance;
            }
            set
            {
                currentSelectedAppliance = value;
                OnPropertyChanged("CurrentSelectedAppliance");
            }
        }

        public IEnumerable<Appliance> Appliances
        {
            get
            {
                if(CurrentSelectedCustomer != null)
                {
                    return new ObservableCollection<Appliance>(CurrentSelectedCustomer.FitOut.Appliances);
                }
                return null;
            }
        }

        private DateTime? warrantyEndDate;
        public DateTime? WarrantyEndDate
        {
            get
            {
                if (warrantyEndDate == null)
                {
                    warrantyEndDate = DateTime.Now;
                }
                return warrantyEndDate;
            }
            set
            {
                warrantyEndDate = value;                
                OnPropertyChanged("WarrantyEndDate");
                OnPropertyChanged("WarrantyStatus");
            }
        }

        public string WarrantyStatus
        {
            get
            {
                return UtilityHelper.GetApplianceWarrantyStatus(WarrantyEndDate);
            }
        }

        private DateTime? fitOutDateCompletion;
        public DateTime? FitOutCompletionDate
        {
            get
            {
                if (fitOutDateCompletion == null)
                {
                    fitOutDateCompletion = DateTime.Now;
                }
                return fitOutDateCompletion;
            }
            set
            {
                fitOutDateCompletion = value;
                OnPropertyChanged("FitOutCompletionDate");
                OnPropertyChanged("FitOutWarrantyStatus");
            }
        }

        public string FitOutWarrantyStatus
        {
            get
            {
                return UtilityHelper.GetFitOutWarrantyStatus(FitOutCompletionDate);
            }
        }

        private void CreateAppliance()
        {
            CurrentSelectedAppliance = null;
            WarrantyEndDate = null;
            CurrentSelectedAppliance = new Appliance();
        }

        private void AddAppliance()
        {
            CurrentSelectedAppliance.WarrantyEndDate = WarrantyEndDate;
            CurrentSelectedCustomer.FitOut.Appliances.Add(CurrentSelectedAppliance);            
            context.Entry(CurrentSelectedAppliance).State = EntityState.Added;
            CurrentSelectedAppliance = null;
            WarrantyEndDate = null;
            OnPropertyChanged("Appliances");
    
        }

        private void DeleteAppliance()
        {
            CurrentSelectedCustomer.FitOut.Appliances.Remove(CurrentSelectedAppliance);
            context.Entry(CurrentSelectedAppliance).State = EntityState.Deleted;
            CurrentSelectedAppliance = null;
            OnPropertyChanged("Appliances");
        }

        private void EditUpdateAppliance()
        {
            if(CurrentSelectedAppliance != null)
            {
                CurrentSelectedAppliance.WarrantyEndDate = WarrantyEndDate;
                context.Entry(CurrentSelectedAppliance).State = EntityState.Modified;
                OnPropertyChanged("Appliances");
            }
        }       

        private void EditAppliance()
        {
            if (CurrentSelectedAppliance != null)
            {
                WarrantyEndDate = CurrentSelectedAppliance.WarrantyEndDate;
                OnPropertyChanged("WarrantyEndDate");
            }
        }

        #endregion

        #region AppUser
        private AppUser currentAppUser;
        public AppUser CurrentAppUser
        {
            get
            {               
                return currentAppUser;
            }
            set
            {
                currentAppUser = value;               
            }
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

        ICommand _addBankTypeCommand;
        public ICommand AddBankTypeCommand
        {
            get
            {
                if (_addBankTypeCommand == null)
                {
                    _addBankTypeCommand = new RelayCommand(AddBankType);
                }
                return _addBankTypeCommand;
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

        ICommand _searchUserCommand;
        public ICommand SearchUserCommand
        {
            get
            {
                if (_searchUserCommand == null)
                {
                    _searchUserCommand = new RelayCommand(SearchUser);
                }
                return _searchUserCommand;
            }
        }

        ICommand _logoutUserCommand;
        public ICommand LogoutUserCommand
        {
            get
            {
                if (_logoutUserCommand == null)
                {
                    _logoutUserCommand = new RelayCommand(LogoutUser);
                }
                return _logoutUserCommand;
            }
        }

        ICommand _changeUserPasswordCommand;
        public ICommand ChangeUserPasswordCommand
        {
            get
            {
                if (_changeUserPasswordCommand == null)
                {
                    _changeUserPasswordCommand = new RelayCommand(ChangeUserPassword);
                }
                return _changeUserPasswordCommand;
            }
        }

        ICommand _sendTemporaryPINCommand;
        public ICommand SendTemporaryPINCommand
        {
            get
            {
                if (_sendTemporaryPINCommand == null)
                {
                    _sendTemporaryPINCommand = new RelayCommand(SendTemporaryPIN);
                }
                return _sendTemporaryPINCommand;
            }
        }

        ICommand _verifyResetPasswordCommand;
        public ICommand VerifyResetPasswordCommand
        {
            get
            {
                if (_verifyResetPasswordCommand == null)
                {
                    _verifyResetPasswordCommand = new RelayCommand(VerifyResetPassword);
                }
                return _verifyResetPasswordCommand;
            }
        }

        ICommand _resetPasswordCommand;
        public ICommand ResetPasswordCommand
        {
            get
            {
                if (_resetPasswordCommand == null)
                {
                    _resetPasswordCommand = new RelayCommand(ResetPassword);
                }
                return _resetPasswordCommand;
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

        ICommand _addUtilityBillTypeCommand;
        public ICommand AddUtilityBillTypeCommand
        {
            get
            {
                if (_addUtilityBillTypeCommand == null)
                {
                    _addUtilityBillTypeCommand = new RelayCommand(AddUtilityBillType);
                }
                return _addUtilityBillTypeCommand;
            }
        }

        ICommand _addUtilityCompanyCommand;
        public ICommand AddUtilityCompanyCommand
        {
            get
            {
                if (_addUtilityCompanyCommand == null)
                {
                    _addUtilityCompanyCommand = new RelayCommand(AddUtilityCompany);
                }
                return _addUtilityCompanyCommand;
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

        ICommand _createUtilityBillTypeCommand;
        public ICommand CreateUtilityBillTypeCommand
        {
            get
            {
                if (_createUtilityBillTypeCommand == null)
                {
                    _createUtilityBillTypeCommand = new RelayCommand(CreateUtilityBillType);
                }
                return _createUtilityBillTypeCommand;
            }
        }


        ICommand _createUtilityCompanyCommand;
        public ICommand CreateUtilityCompanyCommand
        {
            get
            {
                if (_createUtilityCompanyCommand == null)
                {
                    _createUtilityCompanyCommand = new RelayCommand(CreateUtilityCompany);
                }
                return _createUtilityCompanyCommand;
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

        ICommand _createBankCommand;
        public ICommand CreateBankCommand
        {
            get
            {
                if (_createBankCommand == null)
                {
                    _createBankCommand = new RelayCommand(CreateBank);
                }
                return _createBankCommand;
            }
        }

        ICommand _editUpdateBankCommand;
        public ICommand EditUpdateBankCommand
        {
            get
            {
                if (_editUpdateBankCommand == null)
                {
                    _editUpdateBankCommand = new RelayCommand(EditUpdateBank);
                }
                return _editUpdateBankCommand;
            }
        }

        ICommand _editBankCommand;
        public ICommand EditBankCommand
        {
            get
            {
                if (_editBankCommand == null)
                {
                    _editBankCommand = new RelayCommand(EditBank);
                }
                return _editBankCommand;
            }
        }

        ICommand _createBankTypeCommand;
        public ICommand CreateBankTypeCommand
        {
            get
            {
                if (_createBankTypeCommand == null)
                {
                    _createBankTypeCommand = new RelayCommand(CreateBankType);
                }
                return _createBankTypeCommand;
            }
        }
       
        ICommand _createApplianceCommand;
        public ICommand CreateApplianceCommand
        {
            get
            {
                if (_createApplianceCommand == null)
                {
                    _createApplianceCommand = new RelayCommand(CreateAppliance);
                }
                return _createApplianceCommand;
            }
        }

        ICommand _addApplianceCommand;
        public ICommand AddApplianceCommand
        {
            get
            {
                if (_addApplianceCommand == null)
                {
                    _addApplianceCommand = new RelayCommand(AddAppliance);
                }
                return _addApplianceCommand;
            }
        }

        ICommand _deleteApplianceCommand;
        public ICommand DeleteApplianceCommand
        {
            get
            {
                if(_deleteApplianceCommand == null)
                {
                    _deleteApplianceCommand = new RelayCommand(DeleteAppliance);
                }
                return _deleteApplianceCommand;
            }
        }

        ICommand _editUpdateApplianceCommand;
        public ICommand EditUpdateApplianceCommand
        {
            get
            {
                if(_editUpdateApplianceCommand == null)
                {
                    _editUpdateApplianceCommand = new RelayCommand(EditUpdateAppliance);
                }
                return _editUpdateApplianceCommand;
            }
        }

        ICommand _editApplianceCommand;
        public ICommand EditApplianceCommand
        {
            get
            {
                if (_editApplianceCommand == null)
                {
                    _editApplianceCommand = new RelayCommand(EditAppliance);
                }
                return _editApplianceCommand;
            }
        }

        ICommand _cancelChangesCommand;
        public ICommand CancelChangesCommand
        {
            get
            {
                if (_cancelChangesCommand == null)
                {
                    _cancelChangesCommand = new RelayCommand(CancelChanges);
                }
                return _cancelChangesCommand;
            }
        }

        ICommand _createPaymentCommand;
        public ICommand CreatePaymentCommand
        {
            get
            {
                if (_createPaymentCommand == null)
                {
                    _createPaymentCommand = new RelayCommand(CreatePayment);
                }
                return _createPaymentCommand;
            }
        }

        ICommand _addPaymentCommand;
        public ICommand AddPaymentCommand
        {
            get
            {
                if (_addPaymentCommand == null)
                {
                    _addPaymentCommand = new RelayCommand(AddPayment);
                }
                return _addPaymentCommand;
            }
        }

        ICommand _deletePaymentCommand;
        public ICommand DeletePaymentCommand
        {
            get
            {
                if (_deletePaymentCommand == null)
                {
                    _deletePaymentCommand = new RelayCommand(DeletePayment);
                }
                return _deletePaymentCommand;
            }
        }

        ICommand _editUpdatePaymentCommand;
        public ICommand EditUpdatePaymentCommand
        {
            get
            {
                if (_editUpdatePaymentCommand == null)
                {
                    _editUpdatePaymentCommand = new RelayCommand(EditUpdatePayment);
                }
                return _editUpdatePaymentCommand;
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
        // Fields 
        
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
                .Include(h => h.Utilities)
                .Include(j => j.TitleInfo)
                .Include(k => k.FitOut);
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

        public static IQueryable<BankType> GetAllBankTypes(this ManagerDBContext context)
        {
            return context.BankTypes;
        }

        public static AppUser GetUserByParam(this ManagerDBContext context, string username, string password)
        {
            AppUser user = null;
            user = context.AppUsers.ToList().Find(u => u.UserName.Equals(username) && u.CurrentPassword.Equals(password));            
            return user;
        }

        public static AppUser GetUserByEmail(this ManagerDBContext context, string emailAd)
        {
            AppUser user = null;
            user = context.AppUsers.ToList().Find(u => u.EmailAddress.Equals(emailAd));
            return user;
        }

        public static AppUser GetUserByEmailandPIN(this ManagerDBContext context, string emailAd, string temporayPIN)
        {
            AppUser user = null;
            user = context.AppUsers.ToList().Find(u => u.EmailAddress.Equals(emailAd) && u.TemporaryPin.Equals(temporayPIN));
            return user;
        }
    }
    #endregion

}

