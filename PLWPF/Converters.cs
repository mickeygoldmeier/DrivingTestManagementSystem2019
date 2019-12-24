using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace PLWPF
{
    /// <summary>
    /// A class that converts Gender to index for a comboBox
    /// </summary>
    public class GenderConverter : IValueConverter
    {
        /// <summary>
        /// Convert from selected index to BE.Gender 
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case BE.Gender.Female:
                    return 1;
                case BE.Gender.Male:
                    return 0;
            }
            // If there is no selected index for some reason, return null
            return null;
        }

        /// <summary>
        /// Convert from BE.Gender to selected index
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 1:
                    return BE.Gender.Female;
                case 0:
                    return BE.Gender.Male;
            }
            // If there is no selected Gender for some reason, return null
            return null;
        }
    }

    /// <summary>
    /// A class that converts CarType to index for a comboBox
    /// </summary>
    public class CarTypeConvertor : IValueConverter
    {
        /// <summary>
        /// Convert from selected index to BE.Car_type 
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case BE.Car_type.Private_car:
                    return 0;
                case BE.Car_type.Two_wheeled_vehicle:
                    return 1;
                case BE.Car_type.Medium_truck:
                    return 2;
                case BE.Car_type.Heavy_truck:
                    return 3;
            }
            // If there is no selected index for some reason, return null
            return null;
        }

        /// <summary>
        /// Convert from BE.Gender to selected index
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 0:
                    return BE.Car_type.Private_car;
                case 1:
                    return BE.Car_type.Two_wheeled_vehicle;
                case 2:
                    return BE.Car_type.Medium_truck;
                case 3:
                    return BE.Car_type.Heavy_truck;
            }
            // If there is no selected Car_type for some reason, return null
            return null;
        }
    }

    /// <summary>
    /// A class that converts GearboxType to index for a comboBox
    /// </summary>
    public class GearboxTypeConvertor : IValueConverter
    {
        /// <summary>
        /// Convert from BE.Gearbox_type to selected index
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case BE.Gearbox_type.Manual:
                    return 0;
                case BE.Gearbox_type.Automatic:
                    return 1;
            }
            // If there is no selected index for some reason, return null
            return null;
        }

        /// <summary>
        /// Convert from selected index to BE.Gearbox_type 
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 0:
                    return BE.Gearbox_type.Manual;
                case 1:
                    return BE.Gearbox_type.Automatic;
            }
            // If there is no selected Gender for some reason, return null
            return null;
        }
    }

    /// <summary>
    /// A class that converts CarType to hebrow string
    /// </summary>
    public class CarTypeConvertorToString : IValueConverter
    {
        /// <summary>
        /// Convert from BE.Car_type to string 
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case BE.Car_type.Private_car:
                    return "רכב פרטי";
                case BE.Car_type.Two_wheeled_vehicle:
                    return "רכב דו-גלגלי";
                case BE.Car_type.Medium_truck:
                    return "משאית בינונית";
                case BE.Car_type.Heavy_truck:
                    return "משאית כבדה";
            }
            // If there is no selected Car_type for some reason, return error
            return "שגיאה";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A class that converts Gender to hebrow string
    /// </summary>
    public class GenderConvertorToString : IValueConverter
    {
        /// <summary>
        /// Convert from BE.Gender to string 
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case BE.Gender.Female:
                    return "נקבה";
                case BE.Gender.Male:
                    return "זכר";
            }
            // If there is no selected Gender for some reason, return error
            return "שגיאה";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A class that converts Gear to hebrow string
    /// </summary>
    public class GearboxTypeConvertorToString : IValueConverter
    {
        /// <summary>
        /// Convert from BE.Gearbox_type to string 
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case BE.Gearbox_type.Manual:
                    return "ידני";
                case BE.Gearbox_type.Automatic:
                    return "אוטומטי";
            }
            // If there is no selected Gearbox_type for some reason, return error
            return "שגיאה";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class QuestionHasImage : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "")
                return "לא";
            return "כן";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
