using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aSati.Shared.Models
{
    public class Lease
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PropertyUnitId { get; set; } // 🔄 from PropertyId to PropertyUnitId
        public PropertyUnit? PropertyUnit { get; set; } // 👈 Add this
        public string TenantId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }
        public LeaseStatus Status { get; set; } = LeaseStatus.Active;
        public List<PropertyChecklistItem> ChecklistItems { get; set; } = new();
    }
    public enum LeaseStatus
    {
        Active,
        Terminated,
        PendingTermination
    }

}
