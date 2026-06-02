using ArchivAI.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Core.Entities
{
    public class AppUser : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}
