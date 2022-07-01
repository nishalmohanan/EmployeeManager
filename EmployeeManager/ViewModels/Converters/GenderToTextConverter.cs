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
    internal class GenderToTextConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Gender gender = (Gender)value;
            return gender.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object gender;
            Enum.TryParse(typeof(Gender), (string)value, true, out gender);
            return Gender.Male;
        }
    }
}
