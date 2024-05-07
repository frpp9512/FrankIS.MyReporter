namespace FrankIS.ClockifyManagement.Configuration;

public record ClockifyConfiguration
{
    public required string ClockifyApiBaseUrl { get; set; }
    public required string ApiKey { get; set; }
}
