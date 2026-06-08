using ArchivAI.Application.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Mscc.GenerativeAI;
using Mscc.GenerativeAI.Types;
using System;
using System.Collections.Generic;
using System.Text;
using UglyToad.PdfPig;

namespace ArchivAI.Infrastructure.Services
{
    public class AIService : IAIService
    {
        private readonly GenerativeModel _model;

        public AIService(IConfiguration configuration)
        {
            var apiKey = configuration["GoogleAi:ApiKey"]??throw new Exception();
            var GoogleAi = new GoogleAI(apiKey);
            _model = GoogleAi.GenerativeModel("gemini-3.5-flash");
        }
        public Task<string> ExtractTextFromFile(string filePath, string extension)
        {
            var text = extension.ToLower() switch
            {
                ".pdf" => ExtractFromPdf(filePath),
                ".txt" => File.ReadAllText(filePath),
                _ => throw new NotSupportedException($"File type {extension} not supported for text extraction.")
            };

            return Task.FromResult(text);
        }

        private string ExtractFromPdf(string filePath)
        {
            var sb = new StringBuilder();// Use PdfPig to extract text from the PDF file    
            using var pdf = PdfDocument.Open(filePath);// Iterate through each page and append the text to the StringBuilder
            foreach (var page in pdf.GetPages())
                sb.AppendLine(page.Text);
            return sb.ToString();
        }

        public async Task<string> SummarizeAsync(string text)
        { 
            var truncatedText = text.Length > 8000 ? text[..8000] : text;// Truncate the text to fit within the model's context window (e.g., 8000 tokens)
            var prompt = $"""
            You are an expert document analyst.
            Summarize the following document clearly and concisely in 3-5 sentences.
            Focus on the main topic, key points, and purpose of the document.
            
            Document:
            {truncatedText}
            """;
            var response = await _model.GenerateContent(prompt);

            return response.Text??"Could not generate summary.";

        }
           
        
    }
}
