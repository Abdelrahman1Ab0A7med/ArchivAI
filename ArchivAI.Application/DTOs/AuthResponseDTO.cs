using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Application.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
