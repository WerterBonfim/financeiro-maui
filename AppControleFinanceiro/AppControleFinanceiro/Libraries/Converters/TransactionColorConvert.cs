using System.Globalization;
using AppControleFinanceiro.Models;

namespace AppControleFinanceiro.Libraries.Converters;

public class TransactionColorConvert : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return null;
        
        var transaction = (Transaction)value!;

        value = transaction.Type == TransactionType.Outcome
            ? Colors.Red
            : Color.FromRgb(147, 158, 90);
       
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}