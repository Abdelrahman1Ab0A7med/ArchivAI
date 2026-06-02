using ArchivAI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Infrastructure.Configurations
{
    public class ChatMessageConfigurations : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(cm => cm.Id);
            builder.Property(cm => cm.Question)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(cm => cm.Answer).IsRequired() ;
            builder.HasOne(cm => cm.Document)
                .WithMany()
                .HasForeignKey(cm => cm.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
