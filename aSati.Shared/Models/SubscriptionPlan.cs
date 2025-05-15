using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aSati.Shared.Models
{
    public class SubscriptionPlan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty; // Free, Standard, Premium
        public decimal Price { get; set; }
        public List<FeatureAccess>? Features { get; set; }
    }
}
