using Microsoft.AspNetCore.Http;

namespace ChatApp.Data.Interfaces;

public interface IImageCompressor
{
    public Task<MemoryStream> Compress(IFormFile image);
}