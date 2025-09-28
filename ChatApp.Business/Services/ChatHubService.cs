using ChatApp.Data.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Business.Services;

public class ChatHubService
{
    private readonly IImageCompressor _imageCompressor;

    public ChatHubService(IImageCompressor imageCompressor)
    {
        _imageCompressor = imageCompressor;
    }

    public async Task Upload(IFormFile image)
    {
        var res = await _imageCompressor.Compress(image);
        
    } 
}