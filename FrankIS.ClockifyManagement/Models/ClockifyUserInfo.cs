namespace FrankIS.ClockifyManagement.Models;

public record ClockifyUserInfo
{
    public required string ActiveWorkspace { get; set; }
    public ClockifyCustomFieldValue[]? CustomFields { get; set; }
    public required string DefaultWorkspace { get; set; }
    public required string Email { get; set; }
    public required string Id { get; set; }
    public ClockifyMembership[]? Memberships { get; set; }
    public required string Name { get; set; }
    public string? ProfilePicture { get; set; }
    public required ClockifyUserSettings Settings { get; set; }
    public required string Status { get; set; }
}
