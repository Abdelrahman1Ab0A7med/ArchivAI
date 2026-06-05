using ArchivAI.Core.Entities;
using ArchivAI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Application.DTOs
{
    public class DocumentResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSizeInBytes { get; set; }
        public DocumentType Type { get; set; }
        public DocumentStatus Status { get; set; } = DocumentStatus.Pending;

        // AI generated fields (we'll fill these in Milestone 4)
        public string? AISummary { get; set; }
        public string? AIEmbedding { get; set; }

        // Who owns this document
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
