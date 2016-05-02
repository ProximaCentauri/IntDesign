using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel()
        {

        }

        #region INotifyPropertyChanged Implementing

        public event PropertyChangedEventHandler PropertyChanged;
                
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Event Handlers

        public ICommand ActionCommand { get; set; }

        public void Dispose()
        {
            if (!dispose)
                dispose = true;
        }

        private bool dispose;
    }
}
