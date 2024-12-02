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
           .HasColumnName("Number")
           .IsRequired();

            builder.Property(e => e.District)
           .HasColumnName("District")
           .IsRequired();

            builder.Property(e => e.City)
           .HasColumnName("City")
           .IsRequired();

            builder.Property(e => e.State)
           .HasColumnName("State")
           .IsRequired();

            builder.Property(e => e.PostalCode)
           .HasColumnName("PostalCode")
           .IsRequired();

            builder.Property(e => e.AddressName)
           .HasColumnName("AddressName")
           .IsRequired();

            builder.Property(e => e.CreateDate)
           .HasColumnName("CreateDate")
           .IsRequired();

            builder.Property(e => e.UpdateDate)
            .HasColumnName("UpdateDate");

            builder.Property(e => e.UserId)
              .HasColumnName("UserId")
              .IsRequired();
            
            builder
              .HasOne(f => f.User)
              .WithMany(p => p.Addresses)
              .HasForeignKey(f => f.UserId)
              .HasPrincipalKey(p => p.Id);

        }
    }
}
