using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace ViewModel
{
    interface IMainViewModel : INotifyPropertyChanged, IDisposable
    {
        ICommand ActionCommand { get; set; }
    }
}
