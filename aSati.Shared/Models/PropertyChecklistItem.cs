using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aSati.Shared.Models
{
    public class PropertyChecklistItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty; // E.g., "Painting"
        public string? ExpectedState { get; set; }       // Default e.g. "Good"
        public string? ActualState { get; set; }
        public string? Comment { get; set; }
        public string? MediaUrl { get; set; }
        public ChecklistStatus Status { get; set; }
    }
}
