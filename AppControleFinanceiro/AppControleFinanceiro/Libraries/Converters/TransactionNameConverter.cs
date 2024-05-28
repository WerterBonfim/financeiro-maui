using System.Globalization;

namespace AppControleFinanceiro.Libraries.Converters
{
    public class TransactionNameConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) 
            => ((string)value!)?.ToUpper()[0];

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
