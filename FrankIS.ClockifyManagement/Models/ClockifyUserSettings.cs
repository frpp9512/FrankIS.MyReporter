namespace FrankIS.ClockifyManagement.Models;

public record ClockifyUserSettings
{
    public bool Alerts { get; set; }
    public bool Approval { get; set; }
    public bool CollapseAllProjectLists { get; set; }
    public bool DashboardPinToTop { get; set; }
    public string? DashboardSelection { get; set; }
    public string? DashboardViewType { get; set; }
    public required string DateFormat { get; set; }
    public bool GroupSimilarEntriesDisabled { get; set; }
    public bool IsCompactViewOn { get; set; }
    public required string Lang { get; set; }
    public bool LongRunning { get; set; }
    public bool MultiFactorEnabled { get; set; }
    public required string MyStartOfDay { get; set; }
    public bool Onboarding { get; set; }
    public int ProjectListCollapse { get; set; }
    public bool ProjectPickerTaskFilter { get; set; }
    public bool Pto { get; set; }
    public bool Reminders { get; set; }
    public bool ScheduledReports { get; set; }
    public bool Scheduling { get; set; }
    public bool SendNewsletter { get; set; }
    public bool ShowOnlyWorkingDays { get; set; }
    public required ClockifySummaryReportSettings SummaryReportSettings { get; set; }
    public required string Theme { get; set; }
    public required string TimeFormat { get; set; }
    public bool TimeTrackingManual { get; set; }
    public required string TimeZone { get; set; }
    public required string WeekStart { get; set; }
    public bool WeeklyUpdates { get; set; }
}
