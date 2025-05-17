using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aSati.Shared.DTOs
{
    public class SystemAdminDashboardDto
    {
        public int TotalUsers { get; set; }
        public int PropertyOwners { get; set; }
        public int StaffMembers { get; set; }
        public int Tenants { get; set; }
        public int TotalProperties { get; set; }
        public int TotalUnits { get; set; }
        public int ActiveLeases { get; set; }
    }
}
