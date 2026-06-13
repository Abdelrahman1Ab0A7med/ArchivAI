using ArchivAI.Application.Interfaces;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Infrastructure.Services
{
    public class BackGroundService : IBackGroundService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public BackGroundService(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }
        public void EnqueueDocumentSummarization(Guid documentId, Guid userId)
        {
            _backgroundJobClient.Enqueue<IDocumentService>(service => service.SummarizeDocumentAsync(documentId, userId));
        }
    }
}
