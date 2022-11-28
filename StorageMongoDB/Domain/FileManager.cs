using StorageMongoDB.Data;
using StorageMongoDB.Models;

namespace StorageMongoDB.Domain
{
    public interface IFileManager
    {
        Task<ICollection<FileDb>> GetAsync();
        Task<BufferedStream?> GetAsync(string id);
        Task CreateAsync(IFormFileCollection newFile);
        Task RemoveAsync(string id);
    }

    public class FileManager : IFileManager
    {
        private readonly IRepoFile _repo;


        public FileManager(IRepoFile repo)
        {
            _repo = repo;
        }

        public async Task<ICollection<FileDb>> GetAsync()
        {
            var result = await _repo.GetAsync();
            return result;
        }

        public async Task<BufferedStream?> GetAsync(string id)
        {
            var result = await _repo.GetAsync(id);
            return result;
        }

        public async Task CreateAsync(IFormFileCollection newFile)
        {
            await _repo.CreateAsync(newFile);
        }

        public async Task RemoveAsync(string id)
        {
            await _repo.RemoveAsync(id);
        }
    }
}
