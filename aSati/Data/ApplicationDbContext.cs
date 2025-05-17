// Ignore Spelling: Sati

using aSati.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace aSati.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<MainProperty> Properties { get; set; }
        public DbSet<PropertyUnit> PropertyUnits { get; set; }
        public DbSet<Lease> Leases { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ✅ MainProperty → PropertyUnit
            builder.Entity<PropertyUnit>()
                .HasOne(u => u.Property)
                .WithMany(p => p.Units)
                .HasForeignKey(u => u.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ PropertyUnit → Lease
            builder.Entity<Lease>()
                .HasOne(l => l.PropertyUnit)
                .WithMany(u => u.Leases)
                .HasForeignKey(l => l.PropertyUnitId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔗 Lease owns ChecklistItems (no separate table mapping)
            builder.Entity<Lease>()
                .OwnsMany(l => l.ChecklistItems, cb =>
                {
                    cb.WithOwner().HasForeignKey("LeaseId");
                    cb.HasKey("LeaseId", "Id"); // Composite key for owned collection
                });

            // 🔗 MainProperty.OwnerId → ApplicationUser.Id
            builder.Entity<MainProperty>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔗 Lease.TenantId → ApplicationUser.Id
            builder.Entity<Lease>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(l => l.TenantId)
                .OnDelete(DeleteBehavior.Restrict); // So tenant can't be deleted if leases exist
        }
    }
}
