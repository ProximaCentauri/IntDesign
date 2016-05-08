using System;
using System.ComponentModel;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    interface IMainViewModel : INotifyPropertyChanged, IDisposable
    {
        ICommand ActionCommand { get; set; }

        void AddCustomer(Customer customer);

        void EditCustomer(Customer customer);

        void DeleteCustomer(Customer customer);
    }
}
