using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using EmployeeManager.Enums;

namespace EmployeeManager.ViewModels.Converters
{
    internal class StatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status gender = (Status)value;
            return gender.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object status;
            Enum.TryParse(typeof(Status), (string)value, true, out status);
            return status;
        }
    }
}
