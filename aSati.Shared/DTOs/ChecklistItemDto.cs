public class ChecklistItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ExpectedState { get; set; }
    public string? ActualState { get; set; }
    public string? Comment { get; set; }
    public string? MediaUrl { get; set; }
    public ChecklistStatus Status { get; set; } = ChecklistStatus.NotStarted;
}
public enum ChecklistStatus
{
    NotStarted,
    InProgress,
    Completed,
    NeedsReview,
    Approve
}

