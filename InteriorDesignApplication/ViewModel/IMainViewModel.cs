using System;
using System.ComponentModel;
using System.Windows.Input;
using Model;
using Model.Controls;
namespace ViewModel
{
    public interface IMainViewModel : INotifyPropertyChanged, IDisposable
    {
        ICommand ActionCommand { get; set; }
    
        int SelectedIndex { get; set; }        
       
        void DeleteCustomer(Customer customer);

        void CreateEntity(object obj);

        void LoadCustomers();
        PopupView CurrentPopupView { get; set; }
    }
}
