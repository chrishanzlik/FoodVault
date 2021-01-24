using FoodVault.Application.FileUploads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodVault.Api.Storage.FileUploads
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadsController : Controller
    {
        private readonly IFileStorage _fileStorage;

        public FileUploadsController(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileTemporaryAsync(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            Guid id;

            try
            {
                id = await _fileStorage.StoreFileTemporaryAsync(file.OpenReadStream(), file.FileName,
                                                                file.ContentType, TimeSpan.FromHours(1));
            }
            catch(UploadFileException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Created(string.Empty, new { id });
        }
    }
}
