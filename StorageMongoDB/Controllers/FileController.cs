using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using StorageMongoDB.Domain;

namespace StorageMongoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileManager _manager;

        public FileController(IFileManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsyncList()
        {
            var result = await _manager.GetAsync();
            return result != null ? Ok(result.ToJson()) : NotFound(result.ToJson());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var result = await _manager.GetAsync(id);

            return result != null
                ? File(result, "application/octet-stream")
                : NotFound();
        }

        [HttpDelete]
        public async Task RemoveAsync(string id)
        {
            await _manager.RemoveAsync(id);
        }

        [HttpPost]
        public async Task CreateAsync(IFormFileCollection newFile)
        {
            await _manager.CreateAsync(newFile);
        }
    }
}
