using ArchivAI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Infrastructure.Data
{
    public class ArchivAIDbContext(DbContextOptions<ArchivAIDbContext> options) : DbContext(options)
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Document> Documents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchivAIDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
