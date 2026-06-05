using ArchivAI.Api.Extensions;
using ArchivAI.Application.DTOs;
using ArchivAI.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArchivAI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;


        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }
        [HttpPost("test")]
        public IActionResult Test()
        {
            return Ok("Working");
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadDocumentDTO file)
        {
            var user = User.GetUserId();
            var result = await _documentService.UploadAsync(file, user);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = User.GetUserId();
            var documents = await _documentService.GetAllAsync(user);
            return Ok(documents);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = User.GetUserId();
            var document = await _documentService.GetByIdAsync(id, user);
            return Ok(document);
        }
    }
}
