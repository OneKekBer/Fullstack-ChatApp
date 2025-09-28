using Microsoft.AspNetCore.Http;

namespace ChatApp.Data.Interfaces;

public interface IFileStorageProvider
{
    public Task Save(MemoryStream file);
    public Task Delete(string name);
    public Task<MemoryStream> Get(string name);
}