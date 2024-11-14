using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
           .HasColumnName("Name")
           .IsRequired();

            builder.Property(e => e.Cpf)
           .HasColumnName("Cpf")
           .IsRequired();

            builder.Property(e => e.DateBirth)
           .HasColumnName("Date_Birth")
           .IsRequired();

            builder.Property(e => e.Phone)
           .HasColumnName("Phone")
           .IsRequired();

            builder.Property(e => e.Email)
           .HasColumnName("Email")
           .IsRequired();

            builder.Property(e => e.Password)
           .HasColumnName("Password")
           .IsRequired();

            builder.Property(e => e.Active)
           .HasColumnName("Active")
           .IsRequired();

            builder.Property(e => e.Create_Date)
           .HasColumnName("Create_Date")
           .IsRequired();

            builder.Property(e => e.Update_Date)
            .HasColumnName("Update_Date");

            builder.Property(e => e.ProfileId)
            .HasColumnName("Id_Profile")
            .IsRequired();

            builder
            .HasOne(f => f.Profiles)
            .WithMany(p => p.Users)
            .HasForeignKey(f => f.ProfileId)
            .HasPrincipalKey(p => p.Id);
        }
    }
}
