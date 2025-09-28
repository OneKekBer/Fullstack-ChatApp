using ChatApp.Data.Interfaces;

namespace ChatApp.Infrastructure.StorageProviders.S3;

public class S3StorageProvider : IFileStorageProvider
{
    public Task Save(MemoryStream file)
    {
        throw new NotImplementedException();
    }

    public Task Delete(string name)
    {
        throw new NotImplementedException();
    }

    public Task<MemoryStream> Get(string name)
    {
        throw new NotImplementedException();
    }
}