using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Application.DTOs
{
    public class UploadDocumentDTO
    {
        public string Title { get; set; } = string.Empty;
        public IFormFile File { get; set; } = null!;
    }
}
