﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class ProfilePermissionConfig : IEntityTypeConfiguration<ProfilePermissionEntity>
    {
        public void Configure(EntityTypeBuilder<ProfilePermissionEntity> builder)
        {
            builder.ToTable("ProfilePermission");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ProfileId)
            .HasColumnName("ProfileId")
            .IsRequired();

            builder.Property(e => e.PermissionId)
            .HasColumnName("PermissionId")
            .IsRequired();

            builder
            .HasOne(f => f.Profiles)
            .WithMany(p => p.ProfilePermissions)
            .HasForeignKey(f => f.ProfileId)
            .HasPrincipalKey(p => p.Id);

            builder
            .HasOne(f => f.Permissions)
            .WithMany(p => p.ProfilePermissions)
            .HasForeignKey(f => f.PermissionId)
            .HasPrincipalKey(p => p.Id);

        }
    }
}
