using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class ProfileConfig : IEntityTypeConfiguration<ProfileEntity>
    {
        public void Configure(EntityTypeBuilder<ProfileEntity> builder)
        {
            builder.ToTable("Profile");
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

            builder.Property(e => e.CreateDate)
           .HasColumnName("CreateDate")
           .IsRequired();

            builder.Property(e => e.UpdateDate)
            .HasColumnName("UpdateDate");
        }
    }
}
