using aSati.Shared.Models;

public class MainPropertyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<UnitDto> Units { get; set; } = new();
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}

public class UnitDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<LeaseDto> Leases { get; set; } = new();
}
public class LeaseDto
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public LeaseStatus Status { get; set; }
}