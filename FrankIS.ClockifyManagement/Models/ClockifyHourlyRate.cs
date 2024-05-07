namespace FrankIS.ClockifyManagement.Models;

public record ClockifyHourlyRate
{
    public int Amount { get; set; }
    public required string Currency { get; set; }
}
