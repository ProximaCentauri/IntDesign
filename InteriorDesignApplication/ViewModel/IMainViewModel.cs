using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
