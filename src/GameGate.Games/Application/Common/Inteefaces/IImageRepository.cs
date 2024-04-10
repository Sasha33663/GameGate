namespace Application.Common.Inteefaces;

public interface IImageRepository
{
    Task<(string? Id, string? Url)> UploadImageAsync(string? fileName, Stream? fileStream);

    Task DeleteImageAsync(string previewId);
}