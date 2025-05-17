using aSati.Shared.Models;

public class DetailLeaseReviewDto
{
    public Guid LeaseId { get; set; }
    public string TenantEmail { get; set; }
    public string PropertyName { get; set; }
    public string UnitLabel { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public LeaseStatus Status { get; set; }
    public List<ChecklistItemReviewDto> ChecklistItems { get; set; } = new();
}

public class ChecklistItemReviewDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public string? MediaUrl { get; set; }
    public ChecklistStatus Status { get; set; } // e.g. InProgress, Approved, NeedsChanges
}
