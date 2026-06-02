using ArchivAI.Core.Common;

namespace ArchivAI.Core.Entities
{
    public class ChatMessage : BaseEntity
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;

        // Which document was this chat about
        public Guid DocumentId { get; set; }
        public Document Document { get; set; } = null!;
    }
}
