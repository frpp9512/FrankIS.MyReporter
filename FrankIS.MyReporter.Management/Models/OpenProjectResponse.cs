namespace FrankIS.MyReporter.Management.Models;

public record OpenProjectResponse
{
    public int Id { get; set; }
    public required string Subject { get; set; }
    public required DateTime? DerivedStartDate { get; set; }
    public required DateTime? DerivedDueDate { get; set; }
    public int LockVersion { get; set; }
    public bool ScheduleManually { get; set; }
    public required DateTime? StartDate { get; set; }
    public required DateTime? DueDate { get; set; }
    public required DateTime? EstimatedTime { get; set; }
    public required DateTime? DerivedEstimatedTime { get; set; }
    public required DateTime? RemainingTime { get; set; }
    public required DateTime? DerivedRemainingTime { get; set; }
    public required TimeSpan? Duration { get; set; }
    public bool IgnoreNonWorkingDays { get; set; }
    public int PercentageDone { get; set; }
    public int DerivedPercentageDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public record Embedded
{
    public required Attachments Attachments { get; set; }
    public required Relations Relations { get; set; }
    public required Type Type { get; set; }
    public required Priority Priority { get; set; }
    public required Project Project { get; set; }
    public required Author Author { get; set; }
    public required Version Version { get; set; }
}

public record Attachments
{
    public int Total { get; set; }
    public int Count { get; set; }
}

public record Relations
{
    public int Total { get; set; }
    public int Count { get; set; }
}

public record Type
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    public int Position { get; set; }
    public bool IsDefault { get; set; }
    public bool IsMilestone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public record Priority
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Position { get; set; }
    public required string Color { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
}

public record Project
{
    public int Id { get; set; }
    public required string Identifier { get; set; }
    public required string Name { get; set; }
    public bool Active { get; set; }
    public required Description Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required StatusExplanation StatusExplanation { get; set; }
}

public record Description
{
    public required string Format { get; set; }
    public required string Raw { get; set; }
    public required string Html { get; set; }
}

public record StatusExplanation
{
    public required string Format { get; set; }
    public required string Raw { get; set; }
    public required string Html { get; set; }
}

public record Author
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Avatar { get; set; }
}

public record Version
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime? StartDate { get; set; }
    public required DateTime? EndDate { get; set; }
    public required string Status { get; set; }
    public required string Sharing { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
