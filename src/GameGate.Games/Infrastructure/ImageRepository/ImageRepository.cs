using Application.Common.Intefaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Infrastructure.ImageRepository;

public class ImageRepository : IImageRepository
{
    private static readonly Cloudinary _cloudinary; 

    static ImageRepository() 
    {
        var account = new Account(
           "dllpfv6ya",
           "999261638724124",
           "1vC3JrZiFNxqCgzZrqyPN0GCHRA");

        _cloudinary = new Cloudinary(account); 
    }

    public async Task DeleteImageAsync(string previewId)
    {
        var deleteResult = new DeletionParams(previewId);
        await _cloudinary.DestroyAsync(deleteResult);
    }

    public async Task<(string? Id, string? Url)> UploadImageAsync(string? fileName, Stream? fileStream)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(fileName, fileStream)
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        var Id = uploadResult.PublicId.ToString();
        var Url = uploadResult.SecureUrl.ToString();
        return (Id, Url);
    }
}