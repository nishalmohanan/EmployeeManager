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
    internal class GenderToRadioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             Gender  gender = (Gender)value;
            return (gender == (Gender)int.Parse(parameter.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return  parameter;
        }
    }
}
