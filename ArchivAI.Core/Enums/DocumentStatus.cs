using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Core.Enums
{
    public enum DocumentStatus
    {
        Pending = 0,      // The document is waiting to be processed.
        Processing = 1,   // The document is currently being processed.
        Completed = 2,    // The document has been processed successfully.
        Failed = 3        // The document processing has failed.
    }
}
