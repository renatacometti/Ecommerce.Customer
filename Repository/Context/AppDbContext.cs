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
        public virtual DbSet<Domain.Entities.Usuario> Usuario { get; set; }
        public virtual DbSet<Domain.Entities.Endereco> Endereco { get; set; }
        public virtual DbSet<Domain.Entities.Perfil> Perfil { get; set; }
        public virtual DbSet<Domain.Entities.Permissao> Permissao { get; set; }
        public virtual DbSet<Domain.Entities.Perfil_Permissao> Perfil_Permissao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Perfil_Permissao>().ToTable("Perfil_Permissao");

            modelBuilder.Entity<Perfil_Permissao>()
                .HasOne(p => p.Perfil)
                .WithMany(e => e.Perfil_Permissao)
                .HasForeignKey(p => p.Id_Perfil);

            modelBuilder.Entity<Perfil_Permissao>()
                .HasOne(pe => pe.Permissao)
                .WithMany(pp => pp.Perfil_Permissao)
                .HasForeignKey(pe => pe.Id_Permissao);


        }
    }
}
