using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Model.Helpers
{
    public class UtilityStatusConverter : DependencyObject, IValueConverter
    {
        public static DependencyProperty BindableConverterParameterProperty =
             DependencyProperty.Register("BindableConverterParameter", typeof(string),
             typeof(UtilityStatusConverter));

        public string BindableConverterParameter
        {
            get { return (string)GetValue(BindableConverterParameterProperty); }
            set { SetValue(BindableConverterParameterProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                value = "Not Due Yet";
                int result = DateTime.Compare(DateTime.Today.Date, ((DateTime)parameter).Date);                
                if (result > 0)
                {
                    value = "Overdue";
                }
                else if (result == 0)
                {
                    value = "Due Today";
                }
                
            }
            catch (Exception)
            {
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        } 
    }
}
