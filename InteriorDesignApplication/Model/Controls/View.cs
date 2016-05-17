using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Model.Controls
{
    public abstract class View : Page
    {
        public View()
        {
            
        }

        public virtual void Show(bool isShowing)
        {
        }
    }
}
