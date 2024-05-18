namespace FrankIS.ClockifyManagement.Models;

public record ClockifyWorkspaceSettings
{
    public string[] AdminOnlyPages { get; set; } = [];
    public ClockifyAutomaticLock? AutomaticLock { get; set; }
    public bool CanSeeTimeSheet { get; set; }
    public bool CanSeeTracker { get; set; }
    public string? CurrencyFormat { get; set; }
    public bool DecimalFormat { get; set; }
    public bool DefaultBillableProjects { get; set; }
    public bool ForceDescription { get; set; }
    public bool ForceProjects { get; set; }
    public bool ForceTags { get; set; }
    public bool ForceTasks { get; set; }
    public bool IsProjectPublicByDefault { get; set; }
    public string? LockTimeEntries { get; set; }
    public string? LockTimeZone { get; set; }
    public bool MultiFactorEnabled { get; set; }
    public string? NumberFormat { get; set; }
    public bool OnlyAdminsCreateProject { get; set; }
    public bool OnlyAdminsCreateTag { get; set; }
    public bool OnlyAdminsCreateTask { get; set; }
    public bool OnlyAdminsSeeAllTimeEntries { get; set; }
    public bool OnlyAdminsSeeBillableRates { get; set; }
    public bool OnlyAdminsSeeDashboard { get; set; }
    public bool OnlyAdminsSeePublicProjectsEntries { get; set; }
    public bool ProjectFavorites { get; set; }
    public string? ProjectGroupingLabel { get; set; }
    public bool ProjectPickerSpecialFilter { get; set; }
    public ClockifyRound? Round { get; set; }
    public bool TimeRoundingInReports { get; set; }
    public string? TimeTrackingMode { get; set; }
    public bool TrackTimeDownToSecond { get; set; }
}
