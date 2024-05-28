using System.Globalization;
using AppControleFinanceiro.Models;

namespace AppControleFinanceiro.Libraries.Converters;

public class TransactionValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return null;
            
        var transaction = (Transaction)value!;

        value = transaction.Type == TransactionType.Outcome 
            ? $"-{transaction.Value:C}" 
            : $"+{transaction.Value:C}";
       
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}