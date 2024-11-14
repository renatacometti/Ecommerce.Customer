using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class AddressConfig : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Street)
           .HasColumnName("Street")
           .IsRequired();

            builder.Property(e => e.Number)
           .HasColumnName("Cpf")
           .IsRequired();

            builder.Property(e => e.District)
           .HasColumnName("Date_Birth")
           .IsRequired();

            builder.Property(e => e.City)
           .HasColumnName("Phone")
           .IsRequired();

            builder.Property(e => e.State)
           .HasColumnName("Email")
           .IsRequired();

            builder.Property(e => e.PostalCode)
           .HasColumnName("Password")
           .IsRequired();

            builder.Property(e => e.AddressName)
           .HasColumnName("Active")
           .IsRequired();

            builder.Property(e => e.Create_Date)
           .HasColumnName("Create_Date")
           .IsRequired();

            builder.Property(e => e.Update_Date)
            .HasColumnName("Update_Date");

            builder.Property(e => e.UserId)
              .HasColumnName("Id_Profile")
              .IsRequired();
            
            builder
              .HasOne(f => f.Users)
              .WithMany(p => p.Addresses)
              .HasForeignKey(f => f.UserId)
              .HasPrincipalKey(p => p.Id);

        }
    }
}
