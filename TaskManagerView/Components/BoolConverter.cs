using System;
using System.Globalization;
using System.Windows.Data;

namespace TaskManagerView.Components
{
    public class BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            var stateNames = (parameter as string)?.Split(';');
            var boolValue = value as bool?;

            if (stateNames == null || stateNames.Length != 2)
                return null;

            if (boolValue == null)
                return null;

            return stateNames[System.Convert.ToInt32(boolValue.Value)];
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            var stateNames = (parameter as string)?.Split(';');

            if (stateNames == null || stateNames.Length != 2)
                return null;

            if (stateNames[0].Equals(value))
                return false;

            if (stateNames[1].Equals(value))
                return true;

            return null;
        }
    }
}
