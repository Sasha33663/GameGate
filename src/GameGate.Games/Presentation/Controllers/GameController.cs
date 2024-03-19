using Application.Commands.Create;
using Application.Commands.Delete;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace Presentation.Controllers;
[ApiController]
[Route("api/games")]
public class GameController : Controller
{
    private readonly ISender _sender;

    public GameController(ISender sender)
    {
        _sender = sender;
    }
    //[Authorize(Roles="Seller","Admin")]
    [HttpPost("Create")]
    public async Task CreateAsync([FromForm] CreateGameDto createGameDto, CancellationToken cancellationToken)
    {
        var cookie = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key.StartsWith(".AspNetCore.Identity.Application"));
        if (createGameDto.GamePreview == null)
        {
            throw new Exception("Game preview cannot be empty");
        }
        using Stream? stream = createGameDto.GamePreview.OpenReadStream();
        if (!string.IsNullOrEmpty(cookie.Value))
        {
            var cookieString = $"{cookie.Key}={cookie.Value}";
            await _sender.Send(new CreateCommand(createGameDto.Name, createGameDto.Description, createGameDto.GamePreview.FileName, stream, createGameDto.Genre,
            createGameDto.Kind, createGameDto.Creator, cookieString), cancellationToken);
        }
    }
    //[Authorize(Roles="Seller","Admin")]
    [HttpPost("Delete")]
    public async Task DeleteAsync([FromForm] DeleteGameDto deleteGameDto)
    {
        await _sender.Send(new DeleteCommand(deleteGameDto.GameId));
    }

}
