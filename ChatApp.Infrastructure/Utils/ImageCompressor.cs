using ChatApp.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace ChatApp.Infrastructure.Utils;


public class ImageCompressor : IImageCompressor
{
    public async Task<MemoryStream> Compress(IFormFile image)
    {
        var stream = image.OpenReadStream();
        var loaded = await Image.LoadAsync(stream);
        
        var ms = new MemoryStream();
        await loaded.SaveAsync(ms, new WebpEncoder
        {
            FileFormat = WebpFileFormatType.Lossy
        });
        ms.Position = 0;
        
        return ms;
    }
}