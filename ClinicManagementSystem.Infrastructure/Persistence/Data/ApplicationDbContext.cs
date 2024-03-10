using ClinicManagementSystem.Core.Entities.Identities;
using ClinicManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ClinicManagementSystem.Core.Entities.Base;

namespace ClinicManagementSystem.Infrastructure.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;

        public DbSet<Admin> Admins { get; set; } = null!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTimeOffset.UtcNow;
                }

                entry.Entity.UpdatedOn = DateTimeOffset.UtcNow;
            }

            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result;
        }

        public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();

    }
}
