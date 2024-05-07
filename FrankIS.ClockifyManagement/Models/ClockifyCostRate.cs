namespace FrankIS.ClockifyManagement.Models;

public record ClockifyCostRate
{
    public int Amount { get; set; }
    public required string Currency { get; set; }
}
