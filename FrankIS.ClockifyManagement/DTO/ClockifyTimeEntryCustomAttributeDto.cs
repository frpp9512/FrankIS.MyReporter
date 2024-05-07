namespace FrankIS.ClockifyManagement.DTO;

public record ClockifyTimeEntryCustomAttributeDto
{
    public required string Name { get; set; }
    public required string Namespace { get; set; }
    public string? Value { get; set; }
}
