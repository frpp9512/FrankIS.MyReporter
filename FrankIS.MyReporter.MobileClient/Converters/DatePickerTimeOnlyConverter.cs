using System.Globalization;

namespace FrankIS.MyReporter.MobileClient.Converters;
internal class DatePickerTimeOnlyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is DateOnly date ? new DateTime(date, TimeOnly.MinValue) : (object)DateTime.Now;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is DateTime dateTime ? DateOnly.FromDateTime(dateTime) : (object)DateOnly.FromDateTime(DateTime.Now);
    }
}
