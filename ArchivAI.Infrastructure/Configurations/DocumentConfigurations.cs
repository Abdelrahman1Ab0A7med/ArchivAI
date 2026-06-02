using ArchivAI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Infrastructure.Configurations
{
    public class DocumentConfigurations : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(d => d.OriginalFileName).IsRequired();
            builder.HasOne(d=>d.AppUser)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
