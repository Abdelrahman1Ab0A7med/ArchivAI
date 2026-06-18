using ArchivAI.Application.DTOs;
using ArchivAI.Application.DTOs.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentResponseDTO> UploadAsync(UploadDocumentDTO documentDto , Guid UserId);
        Task<DocumentResponseDTO> SummarizeDocumentAsync(Guid documentId, Guid userId); // NEW
        Task<PaginatedResult<DocumentResponseDTO>> GetAllAsync(DocumentQueryDTO documentQuery,Guid UserId);
        Task<DocumentResponseDTO> GetByIdAsync(Guid id, Guid UserId);
        Task<bool> DeleteAsync(Guid id, Guid UserId);
        Task<ChatResponseDTO> ChatWithDocumentAsync(ChatRequestDTO chatDto,Guid documentId,Guid userId);
        Task<List<ChatResponseDTO>> GetChatHistoryAsync(Guid userId , Guid documentId);

    }
}
