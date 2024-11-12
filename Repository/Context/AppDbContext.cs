using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Repository.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {

        }
        public virtual DbSet<Domain.Entities.User> Usuario { get; set; }
        public virtual DbSet<Domain.Entities.Address> Endereco { get; set; }
        public virtual DbSet<Domain.Entities.Profile> Perfil { get; set; }
        public virtual DbSet<Domain.Entities.Permission> Permissao { get; set; }
        public virtual DbSet<Domain.Entities.PermissionProfile> Perfil_Permissao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionProfile>().ToTable("Perfil_Permissao");

            modelBuilder.Entity<PermissionProfile>()
                .HasOne(p => p.Profile)
                .WithMany(e => e.PermissionProfile)
                .HasForeignKey(p => p.ProfileId);

            modelBuilder.Entity<PermissionProfile>()
                .HasOne(pe => pe.Permission)
                .WithMany(pp => pp.PermissionProfile)
                .HasForeignKey(pe => pe.PermissionId);


        }
    }
}
