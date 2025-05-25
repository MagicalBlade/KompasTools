using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static KompasTools.Classes.Sundry.Welding.WeldEnum;

namespace KompasTools.Classes.Sundry.Welding
{
    internal class RadioBoolConverterToLocationPart : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LocationPart locationPart = (LocationPart)value;
            if (locationPart == (LocationPart)Enum.Parse(typeof(LocationPart), parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (LocationPart)Enum.Parse(typeof(LocationPart), parameter.ToString());
        }
    }
}
