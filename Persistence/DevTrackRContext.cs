using DevTrackr.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevTrackr.API.Persistance
{
    public class DevTrackRContext : DbContext
    {
        public DevTrackRContext(DbContextOptions<DevTrackRContext> options)
         : base(options)
        {}

        public DbSet<Package> Packages {get ; set;}
        public DbSet<PackageUpdate> PackageUpdates {get ; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>(e => 
            {
                e.ToTable("tb_Package");
                e.HasKey(e => e.Id);

                e.HasMany(e => e.Updates)
                .WithOne()
                .HasForeignKey(pu => pu.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PackageUpdate>(e =>
            {
                e.HasKey(p => p.Id);
            });
        }
    }
}