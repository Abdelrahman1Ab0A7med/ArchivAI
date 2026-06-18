namespace ArchivAI.Application.Interfaces
{
    public interface IAIService
    {
        Task<string> SummarizeAsync(string text);
        Task<string> ExtractTextFromFile(string filePath , string extension);
        Task<string> ChatWithDocument(string text,string question,List<(string Question , string Answer)> history);

    }
}
