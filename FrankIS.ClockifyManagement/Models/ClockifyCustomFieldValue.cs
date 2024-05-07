namespace FrankIS.ClockifyManagement.Models;

public record ClockifyCustomFieldValue
{
    public required string CustomFieldId { get; set; }
    public required string Name { get; set; }
    public required string TimeEntryId { get; set; }
    public required string Type { get; set; }
    public string? Value { get; set; }
}
