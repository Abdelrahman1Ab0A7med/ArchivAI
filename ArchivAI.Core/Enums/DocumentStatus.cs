using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Core.Enums
{
    public enum DocumentStatus
    {
        Pending = 0,      // The document is waiting to be processed.
        Processing = 1,   // The document is currently being processed.
        Ready = 2,
        Completed = 3,    // The document has been processed successfully.
        Failed = 4        // The document processing has failed.
    }
}
