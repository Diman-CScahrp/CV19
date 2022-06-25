using System.Windows.Data;
using System.Windows;
using System;
using System.Globalization;

namespace CV19.Infrastructure.Converters
{
    internal class ParametricAddValueConverter : DependencyObject, IValueConverter
    {
        #region Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(ParametricAddValueConverter),
                new PropertyMetadata(
                    default(double)
                    ));
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion
        public object Convert(object v, Type t, object p, CultureInfo c)
        {
            var value = System.Convert.ToDouble(v, c);
            return value + Value;
        }

        public object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            var value = System.Convert.ToDouble(v, c);

            return value - Value;
        }
    }
}
