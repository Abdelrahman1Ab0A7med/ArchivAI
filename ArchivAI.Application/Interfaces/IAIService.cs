namespace ArchivAI.Application.Interfaces
{
    public interface IAIService
    {
        Task<string> SummarizeAsync(string text);
        Task<string> ExtractTextFromFile(string filePath , string extension);

    }
}
