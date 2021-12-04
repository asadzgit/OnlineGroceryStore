using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace OnlineGroceryStore.Converter
{
    //A converter class implementing IMultiValueConverter Interface to be used in conversion in binding
    class MyConverter : IMultiValueConverter 
    {
        //Overriding Convert() function of IMultiValueConverter Interface 
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) 
        {
            return values.Clone(); //Creating a Shallow Copying of object values array
        }

        //ConvertBack() function of IMultiValueConverter Interface 
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
