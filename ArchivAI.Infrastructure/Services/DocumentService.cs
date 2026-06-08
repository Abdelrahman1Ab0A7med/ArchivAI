using ArchivAI.Application.DTOs;
using ArchivAI.Application.Interfaces;
using ArchivAI.Core.Entities;
using ArchivAI.Core.Enums;
using ArchivAI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArchivAI.Infrastructure.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ArchivAIDbContext _context;
        private readonly string _uploadsPath;
        private readonly IAIService _aiService;

        public DocumentService(ArchivAIDbContext context, IAIService aiService)
        {
            _context = context;
            _aiService = aiService; 
            _uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(_uploadsPath))
            {
                Directory.CreateDirectory(_uploadsPath);
            }
        }
        public Task<bool> DeleteAsync(Guid id, Guid UserId)
        {
            var document = _context.Documents.FirstOrDefault(d => d.Id == id && d.AppUserId == UserId);
            if (document == null) return Task.FromResult(false);
            if (File.Exists(document.FilePath))
            {
                File.Delete(document.FilePath);
            }

            _context.Documents.Remove(document);
            _context.SaveChanges();

            return Task.FromResult(true);
        }

        public async Task<List<DocumentResponseDTO>> GetAllAsync(Guid UserId)
        {
            return await _context.Documents.Where(d => d.AppUserId == UserId)
                        .Select(d => new DocumentResponseDTO
                        {
                            Id = d.Id,
                            Title = d.Title,
                            OriginalFileName = d.OriginalFileName,
                            FilePath = d.FilePath,
                            FileSizeInBytes = d.FileSizeInBytes,
                            Type = d.Type,
                            Status = d.Status,
                            AISummary = d.AISummary,
                            AppUserId = d.AppUserId
                        }).ToListAsync();
        }

        public async Task<DocumentResponseDTO> GetByIdAsync(Guid id, Guid UserId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.AppUserId == UserId);
            if (document == null)
            {
                throw new InvalidOperationException("Document not found");
            }
            return await MapToDto(document);
        }

        public async Task<DocumentResponseDTO> SummarizeDocumentAsync(Guid documentId, Guid userId)
        {
            var document = _context.Documents.FirstOrDefault(d => d.Id == documentId && d.AppUserId == userId);
            if (document == null)
            {
                throw new InvalidOperationException("Document not found");
            }
            document.Status = DocumentStatus.Processing;
            document.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            try
            {
                
                var text = await _aiService.ExtractTextFromFile(document.FilePath,Path.GetExtension(document.FilePath));
                var summary = await _aiService.SummarizeAsync(text);
                document.AISummary = summary;
                document.Status = DocumentStatus.Ready;
                document.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return await MapToDto(document);

            }
            catch (Exception)
            {
                document.Status = DocumentStatus.Failed;
                document.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                throw;

            }
        }

        

        public async Task<DocumentResponseDTO> UploadAsync(UploadDocumentDTO documentDto, Guid UserId)
        {
            var file = documentDto.File; //catch the file from the DTO
            var extension = Path.GetExtension(file.FileName); //get the file extension
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";//generate a unique file name to avoid conflicts
            var docType = extension.ToLower() switch//determine the document type based on the file extension
            {
                ".pdf" => DocumentType.Pdf,
                ".docx" => DocumentType.Word,
                ".txt" => DocumentType.Text,
                _ => DocumentType.Other
            };
            var filePath = Path.Combine(_uploadsPath, uniqueFileName); //combine the uploads path with the unique file name to get the full file path
            using (var stream = new FileStream(filePath, FileMode.Create)) //save the file to the server
            {
                file.CopyTo(stream);
            }
            var doument = new Document
            {
                Id = Guid.NewGuid(),
                Title = documentDto.Title,
                OriginalFileName = file.FileName,
                FilePath = filePath,
                FileSizeInBytes = file.Length,
                Type = docType,
                Status = DocumentStatus.Pending,
                AppUserId = UserId
            };
            _context.Documents.Add(doument); //add the document to the database context
            _context.SaveChanges(); //save the changes to the database
            return await MapToDto(doument);
        }

        private async Task<DocumentResponseDTO> MapToDto(Document doument) => new DocumentResponseDTO
        {
            Id = doument.Id,
            Title = doument.Title,
            OriginalFileName = doument.OriginalFileName,
            FileSizeInBytes = doument.FileSizeInBytes,
            Type = doument.Type,
            Status = doument.Status,
            AISummary = doument.AISummary,
        };
    }
}
