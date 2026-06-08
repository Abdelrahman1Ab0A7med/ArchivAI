using ArchivAI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentResponseDTO> UploadAsync(UploadDocumentDTO documentDto , Guid UserId);
        Task<DocumentResponseDTO> SummarizeDocumentAsync(Guid documentId, Guid userId); // NEW
        Task<List<DocumentResponseDTO>> GetAllAsync(Guid UserId);
        Task<DocumentResponseDTO> GetByIdAsync(Guid id, Guid UserId);
        Task<bool> DeleteAsync(Guid id, Guid UserId);

    }
}
