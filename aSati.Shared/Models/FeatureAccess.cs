using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aSati.Shared.Models
{
    public class FeatureAccess
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FeatureName { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
    }
}
