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

            // 🔗 MainProperty to PropertyUnit
            builder.Entity<MainProperty>()
                .HasMany(p => p.Units)
                .WithOne()
                .HasForeignKey(u => u.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔗 PropertyUnit to Lease
            builder.Entity<PropertyUnit>()
                .HasMany(u => u.Leases)
                .WithOne()
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
