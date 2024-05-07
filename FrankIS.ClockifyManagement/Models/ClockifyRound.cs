using System.Text.Json.Serialization;

namespace FrankIS.ClockifyManagement.Models;

public class ClockifyRound
{
    public string? Minutes { get; set; }

    [JsonPropertyName("round")]
    public string? RoundValue { get; set; }
}
