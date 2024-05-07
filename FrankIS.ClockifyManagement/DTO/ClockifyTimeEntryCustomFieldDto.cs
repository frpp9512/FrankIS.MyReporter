namespace FrankIS.ClockifyManagement.DTO;

public record ClockifyTimeEntryCustomFieldDto
{
    public string CustomFieldId { get; set; } = Guid.NewGuid().ToString("N");
    public required string SourceType { get; set; }
    public string? Value { get; set; }
}
