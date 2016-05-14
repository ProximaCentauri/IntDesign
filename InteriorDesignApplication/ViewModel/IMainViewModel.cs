using System;
using System.ComponentModel;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    interface IMainViewModel : INotifyPropertyChanged, IDisposable
    {
        ICommand ActionCommand { get; set; }
    
        int SelectedIndex { get; set; }      

        void DeleteCustomer(Customer customer);
    }
}
