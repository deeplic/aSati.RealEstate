using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aSati.Shared.Models
{
    public class PropertyUnit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty; // E.g., "Room A1"
        public Guid PropertyId { get; set; }
        public MainProperty? Property { get; set; }
        public List<Lease>? Leases { get; set; }
    }
}
