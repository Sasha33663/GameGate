using Application.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace Presentation.Controllers;
[Route("api/games")]
public class GameController : Controller
{
    private readonly ISender _sender;
    private readonly IFormFile _formFile;
    public GameController(ISender sender,IFormFile formFile ) 
    {
        _sender = sender;
        _formFile = formFile;
    }
    //[Authorize(Roles="Selles","Admin")]
    [HttpPost("Create")]
    public async Task CreateAsync([FromForm]CreateGameDto createGameDto, CancellationToken cancellationToken)
    {
        var a = User.Identity.Name;
        await _sender.Send(new CreateCommand(createGameDto.Name, createGameDto.Description, /*gamePreview,*/ createGameDto.Genre, createGameDto.Kind, createGameDto.Creator), cancellationToken);
    } 
}
