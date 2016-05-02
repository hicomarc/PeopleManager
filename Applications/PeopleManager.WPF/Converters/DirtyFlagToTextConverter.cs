using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PeopleManager.Data.Model;
using PeopleManager.WPF.BusinessObjects;

namespace PeopleManager.WPF.Converters
{
    /// <summary>
    /// This converter converts true to a star (*) and false to an empty string.
    /// </summary>
    public class DirtyFlagToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? "*" : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
