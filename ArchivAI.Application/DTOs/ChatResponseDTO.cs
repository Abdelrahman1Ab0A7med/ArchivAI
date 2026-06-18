using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Application.DTOs
{
	public class ChatResponseDTO
	{
		public string Question { get; set; }
		public string Answer { get; set; }
		public DateTime AskedAt { get; set; }
	}
}
