using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Repository.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

        public virtual DbSet<Domain.Entities.UserEntity> User { get; set; }
        public virtual DbSet<Domain.Entities.AddressEntity> Address { get; set; }
        public virtual DbSet<Domain.Entities.ProfileEntity> Profile { get; set; }
        public virtual DbSet<Domain.Entities.PermissionEntity> Permission { get; set; }
        public virtual DbSet<Domain.Entities.ProfilePermissionEntity> ProfilePermission { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PermissionProfile>().ToTable("Perfil_Permissao");

        //    modelBuilder.Entity<PermissionProfile>()
        //        .HasOne(p => p.Profile)
        //        .WithMany(e => e.PermissionProfile)
        //        .HasForeignKey(p => p.ProfileId);

        //    modelBuilder.Entity<PermissionProfile>()
        //        .HasOne(pe => pe.Permission)
        //        .WithMany(pp => pp.PermissionProfile)
        //        .HasForeignKey(pe => pe.PermissionId);


        //}
    }
}
