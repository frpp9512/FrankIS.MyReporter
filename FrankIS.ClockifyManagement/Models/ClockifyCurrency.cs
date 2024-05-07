namespace FrankIS.ClockifyManagement.Models;

public record ClockifyCurrency
{
    public required string Id { get; set; }
    public required string Code { get; set; }
    public bool IsDefault { get; set; }
}
