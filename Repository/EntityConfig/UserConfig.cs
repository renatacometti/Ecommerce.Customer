using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
           .HasColumnName("Name")
           .IsRequired();

            builder.Property(e => e.Cpf)
           .HasColumnName("Cpf")
           .IsRequired();

            builder.Property(e => e.Birthday)
           .HasColumnName("Birthday")
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

            builder.Property(e => e.CreateDate)
           .HasColumnName("CreateDate")
           .IsRequired();

            builder.Property(e => e.UpdateDate)
            .HasColumnName("UpdateDate");

            builder.Property(e => e.ProfileId)
            .HasColumnName("ProfileId")
            .IsRequired();

            builder
            .HasOne(f => f.Profile)
            .WithMany(p => p.Users)
            .HasForeignKey(f => f.ProfileId)
            .HasPrincipalKey(p => p.Id);
        }
    }
}
