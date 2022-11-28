using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using StorageMongoDB.Models;

namespace StorageMongoDB.Data
{
    public interface IRepoFile
    {
        Task<ICollection<FileDb>> GetAsync();
        Task<BufferedStream?> GetAsync(string id);
        Task CreateAsync(IFormFileCollection newFile);
        Task RemoveAsync(string id);
    }

    public class RepoFile : IRepoFile
    {
        private readonly IGridFSBucket _gridFs;

        public RepoFile(IMongoDatabase mongo)
        {
            _gridFs = new GridFSBucket(mongo);
        }

        public async Task<ICollection<FileDb>> GetAsync()
        {
            var result = await _gridFs.FindAsync("{}");
            return (await result.ToListAsync()).Select(item => new FileDb
                {
                    Id = item.Id,
                    Name = item.Filename,
                    Description = item.Metadata
                }).
                ToList();
        }

        public async Task<BufferedStream?> GetAsync(string id)
        {
            await using BufferedStream? result = null;
            try
            {
                await _gridFs.DownloadToStreamAsync(new ObjectId(id), result);
            }
            catch (Exception e)
            {
                await Console.Error.WriteLineAsync(e.Message);
            }

            return result;
        }

        public async Task CreateAsync(IFormFileCollection newFile)
        {
            foreach (var file in newFile)
            {
                await using var stream = file.OpenReadStream();
                await _gridFs.UploadFromStreamAsync(file.FileName, stream);
            }
        }

        public async Task RemoveAsync(string id)
        {
            try
            {
                await _gridFs.DeleteAsync(new ObjectId(id));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
