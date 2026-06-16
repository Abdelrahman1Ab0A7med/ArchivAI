namespace ArchivAI.Application.DTOs.Document
{
    public class DocumentQueryDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTitle { get; set; }
    }
}
