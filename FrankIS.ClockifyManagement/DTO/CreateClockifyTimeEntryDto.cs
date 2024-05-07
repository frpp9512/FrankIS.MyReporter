namespace FrankIS.ClockifyManagement.DTO;

public record CreateClockifyTimeEntryDto
{
    public bool Billable { get; set; }
    public ClockifyTimeEntryCustomAttributeDto[]? CustomAttributes { get; set; }
    public ClockifyTimeEntryCustomFieldDto[]? CustomFields { get; set; }
    public string? Description { get; set; }
    public DateTime Start { get; set; } = DateTime.Now;
    public DateTime End { get; set; } = DateTime.Now.AddHours(8);
    public required string ProjectId { get; set; }
    public required string[] TagIds { get; set; }
    public string? TaskId { get; set; }
    public string Type { get; set; } = "REGULAR";
}
