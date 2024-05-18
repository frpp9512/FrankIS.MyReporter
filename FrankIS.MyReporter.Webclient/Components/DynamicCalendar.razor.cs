using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;
using DateRange = (System.DateOnly from, System.DateOnly to);

namespace FrankIS.MyReporter.Webclient.Components;

public partial class DynamicCalendar
{
    [Parameter] public string? Title { get; set; }
    [Parameter] public int Month { get; set; } = DateTime.Now.Month;
    [Parameter] public int Year { get; set; } = DateTime.Now.Year;

    [Parameter] public DateOnly? SelectedDate { get; set; }
    [Parameter] public EventCallback<DateOnly?> SelectedDateChanged { get; set; }

    [Parameter] public DateRange? SelectedDateRange { get; set; }
    [Parameter] public EventCallback<DateRange?> SelectedDateRangeChanged { get; set; }

    private readonly Dictionary<(int month, int year), List<DateOnly?[]>> _generatedMonths = [];
    private List<DateOnly?[]>? _selectedMonth;

    protected override void OnParametersSet()
    {
        _selectedMonth = GetOrGenerateMonth(Month, Year);
        if (SelectedDate is DateOnly selectedDate && SelectedDateRange is null)
        {
            SelectedDateRange = (selectedDate, selectedDate);
        }
    }

    private void IncreaseMonth()
    {
        Month++;
        if (Month >= 12)
        {
            Month = 1;
            Year++;
        }

        _selectedMonth = GetOrGenerateMonth(Month, Year);
        StateHasChanged();
    }

    private void DecreaseMonth()
    {
        Month--;
        if (Month <= 0)
        {
            Month = 12;
            Year--;
        }

        _selectedMonth = GetOrGenerateMonth(Month, Year);
        StateHasChanged();
    }

    private void GoToSelectedDate()
    {
        if (SelectedDate is null)
        {
            return;
        }

        Year = SelectedDate?.Year ?? DateTime.Now.Year;
        Month = SelectedDate?.Month ?? DateTime.Now.Month;
        _selectedMonth = GetOrGenerateMonth(Month, Year);
        StateHasChanged();
    }

    private async Task SelectDate(DateOnly? date, MouseEventArgs mouseEventArgs)
    {
        if (date is not DateOnly newSelectedDate)
        {
            SelectedDateRange = null;
            SelectedDate = null;
            await SelectedDateRangeChanged.InvokeAsync(SelectedDateRange);
            await SelectedDateChanged.InvokeAsync(SelectedDate);
            return;
        }

        if (mouseEventArgs.ShiftKey && SelectedDate is DateOnly selectedDate)
        {
            if (SelectedDateRange is DateRange range && range.from < newSelectedDate)
            {
                SelectedDateRange = (range.from, newSelectedDate);
            }
            else
            {
                SelectedDateRange = (selectedDate < newSelectedDate) ? (selectedDate, newSelectedDate) : (newSelectedDate, selectedDate);
            }
        }
        else
        {
            SelectedDateRange = (newSelectedDate, newSelectedDate);
        }

        SelectedDate = newSelectedDate;
        Month = newSelectedDate.Month;
        Year = newSelectedDate.Year;

        await SelectedDateRangeChanged.InvokeAsync(SelectedDateRange);
        await SelectedDateChanged.InvokeAsync(SelectedDate);

        StateHasChanged();
    }

    private async Task SelectMonthRange()
    {
        SelectedDate = new DateOnly(Year, Month, 1);
        if (SelectedDate is not DateOnly selectedDate)
        {
            return;
        }

        SelectedDateRange = new DateRange(selectedDate, new DateOnly(Year, Month, DateTime.DaysInMonth(Year, Month)));

        await SelectedDateRangeChanged.InvokeAsync(SelectedDateRange);
        await SelectedDateChanged.InvokeAsync(SelectedDate);

        StateHasChanged();
    }

    private async Task SelectWeekRange(DateOnly from, DateOnly to)
    {
        SelectedDate = from;
        SelectedDateRange = (from, to);

        await SelectedDateRangeChanged.InvokeAsync(SelectedDateRange);
        await SelectedDateChanged.InvokeAsync(SelectedDate);
    }

    private List<DateOnly?[]> GetOrGenerateMonth(int month, int year)
    {
        if (_generatedMonths.ContainsKey((month, year)))
        {
            return _generatedMonths[(month, year)];
        }

        List<DateOnly?[]> generatedMonth = GenerateMonth(month, year);
        _generatedMonths.Add((month, year), generatedMonth);
        return generatedMonth;
    }

    private static List<DateOnly?[]> GenerateMonth(int monthToGenerate, int year)
    {
        List<DateOnly?[]> monthWeeks = [];

        int daysInMonth = DateTime.DaysInMonth(year, monthToGenerate);
        int currentDay = 1;

        do
        {
            DateOnly?[] week = new DateOnly?[7];
            int daysAdded = 0;
            int currentDayOfWeek = (int)CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek;
            do
            {
                if (currentDay > daysInMonth)
                {
                    week[currentDayOfWeek] = null;
                    daysAdded++;
                    currentDayOfWeek++;
                    continue;
                }

                DateOnly currentDate = new(year, monthToGenerate, currentDay);
                if ((int)currentDate.DayOfWeek != currentDayOfWeek)
                {
                    week[currentDayOfWeek] = null;
                    daysAdded++;
                    currentDayOfWeek++;
                    continue;
                }

                week[currentDayOfWeek] = currentDate;

                currentDay++;
                daysAdded++;
                currentDayOfWeek++;
                if (currentDayOfWeek > 6)
                {
                    currentDayOfWeek = 0;
                }
            } while (daysAdded < 7);

            monthWeeks.Add(week);
        } while (currentDay <= daysInMonth);

        return monthWeeks;
    }
}
