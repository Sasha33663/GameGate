using Microsoft.AspNetCore.Http;

namespace Presentation.Dto.Create;

public class CreateGameDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile? GamePreview { get; set; }
    public string Genre { get; set; }
    public string Kind { get; set; }
    public string Creator { get; set; }
    public decimal PriceMinValue { get; set; }
    public decimal PriceMaxValue { get; set; }
    public bool IsDirectly { get; set; }
}