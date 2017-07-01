﻿// ActiveTime
// Copyright (C) 2011-2017 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows.Data;

namespace DustInTheWind.ActiveTime.Converters
{
    /// <summary>
    /// Converts the <see cref="TimeSpan"/> value into <see cref="string"/> value and viceversa.
    /// </summary>
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(TimeSpan))
                return null;

            if (value.GetType() == targetType)
                return value;

            if (targetType == typeof(string))
                return ((TimeSpan)value).ToString(@"hh\:mm\:ss");

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || targetType != typeof(TimeSpan))
                return null;

            if (value is string)
                return TimeSpan.Parse((string) value);

            return null;
        }
    }
}