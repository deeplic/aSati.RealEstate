public class LeaseReviewDto
{
    public Guid LeaseId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public string TenantName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool ChecklistCompleted { get; set; }
    public string? OwnerComment { get; set; }
}