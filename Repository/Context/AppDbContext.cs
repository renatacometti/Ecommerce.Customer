using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Repository.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public virtual DbSet<Domain.Entities.UserEntity> Users { get; set; }
        public virtual DbSet<Domain.Entities.AddressEntity> Address { get; set; }
        public virtual DbSet<Domain.Entities.ProfileEntity> Profile { get; set; }
        public virtual DbSet<Domain.Entities.PermissionEntity> Permission { get; set; }
        public virtual DbSet<Domain.Entities.ProfilePermissionEntity> ProfilePermission { get; set; }

      
    }
}
