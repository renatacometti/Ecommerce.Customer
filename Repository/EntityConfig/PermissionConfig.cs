using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class PermissionConfig : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.ToTable("Permission");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
           .HasColumnName("Name")
           .IsRequired();

            builder.Property(e => e.Description)
           .HasColumnName("Description")
           .IsRequired();
  
            builder.Property(e => e.Active)
           .HasColumnName("Active")
           .IsRequired();

            builder.Property(e => e.Create_Date)
           .HasColumnName("Create_Date")
           .IsRequired();

            builder.Property(e => e.Update_Date)
            .HasColumnName("Update_Date");

        }
    }
}
