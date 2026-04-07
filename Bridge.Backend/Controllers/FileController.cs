using Bridge.Backend.Data;
using Bridge.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly BridgeDbContext _context;
        private readonly PythonService _pythonService;

        public FileController(BridgeDbContext context, PythonService pythonService)
        {
            _context = context;
            _pythonService = pythonService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                // Absolute uploads path
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                // Ensure folder exists
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                //  Unique file name
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsPath, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Save initial record
                var fileRecord = new Models.FileRecord
                {
                    FileName = file.FileName,
                    FilePath = filePath,
                    Status = "processing",
                    ExtractedJson = "{}"
                };

                _context.Files.Add(fileRecord);
                await _context.SaveChangesAsync();

                // Run Python extraction
                var extractedJson = _pythonService.RunExtraction(filePath);

                if (string.IsNullOrWhiteSpace(extractedJson))
                {
                    fileRecord.Status = "failed";
                }
                else
                {
                    fileRecord.ExtractedJson = extractedJson;
                    fileRecord.Status = "completed";
                }

                _context.Files.Update(fileRecord);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    fileId = fileRecord.Id,
                    status = fileRecord.Status,
                    data = fileRecord.ExtractedJson ?? "{}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
            }
        }
    }
}