using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Dto;

public class CreateGameDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile? GamePreview {  get; set; }
    public string Genre { get; set; }
    public string Kind { get; set; }
    public string Creator {  get; set; }

}

