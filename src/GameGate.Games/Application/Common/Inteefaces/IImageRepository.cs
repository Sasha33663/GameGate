using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Inteefaces;
public interface IImageRepository
{
    Task<(string? Id, string? Url)> UploadImageAsync(string? fileName, Stream? fileStream);
    Task DeleteImageAsync(string previewId);

}
