using System.Globalization;

namespace FrankIS.MyReporter.MobileClient.Converters;
internal class TimePickerTimeOnlyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TimeOnly time)
        {
            return new TimeSpan(time.Hour, time.Minute, time.Second);
        }
        
        return TimeSpan.Zero;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TimeSpan timeSpan)
        {
            return new TimeOnly(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        return TimeOnly.MinValue;
    }
}
