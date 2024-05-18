namespace FrankIS.MyReporter.Management.Helpers;

public static class DateTimeHelpers
{
    private static DayOfWeek[] WeekendDays => [DayOfWeek.Saturday, DayOfWeek.Sunday];

    public static List<DateOnly> GetWeekDays(int year, int month)
    {
        List<DateOnly> weekDays = [];
        int daysInMonth = DateTime.DaysInMonth(year, month);
        for (int day = 1; day <= daysInMonth; day++)
        {
            DateOnly date = new(year, month, day);
            if (WeekendDays.Contains(date.DayOfWeek))
            {
                continue;
            }

            weekDays.Add(date);
        }

        return weekDays;
    }

    public static List<DateOnly> GetWeekDays(DateOnly from, DateOnly to)
    {
        if (from > to)
        {
            throw new ArgumentException("The starting date must be before the ending date.");
        }

        List<DateOnly> weekDays = [];
        var currentDate = from;
        do
        {
            if (!WeekendDays.Contains(currentDate.DayOfWeek))
            {
                weekDays.Add(currentDate);
            }

            currentDate = currentDate.AddDays(1);
        } while (currentDate <= to);

        return weekDays;
    }
}
