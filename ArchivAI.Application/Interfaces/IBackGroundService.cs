using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Application.Interfaces
{
    public interface IBackGroundService 
    {
        void EnqueueDocumentSummarization(Guid documentId, Guid userId);
    }
}
