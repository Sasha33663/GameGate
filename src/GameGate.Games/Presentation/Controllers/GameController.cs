using Application.Commands.Create;
using Application.Commands.Delete;
using Application.Queries.GetAll;
using Application.Queries.GetByName;
using Application.Queries.GetWithFIlters;
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
            throw new Exception("Game preview can't be empty");
        }
        
        using Stream? stream = createGameDto.GamePreview.OpenReadStream();
        if (!string.IsNullOrEmpty(cookie.Value))
        {   
            var cookieString = $"{cookie.Key}={cookie.Value}";
            await _sender.Send(new CreateCommand(createGameDto.Name, createGameDto.Description,
            createGameDto.GamePreview.FileName, stream, createGameDto.Genre,
            createGameDto.Kind, createGameDto.Creator,createGameDto.PriceMaxValue,
            createGameDto.PriceMinValue,createGameDto.IsDirectly, cookieString), cancellationToken);
        }
    }
    //[Authorize(Roles="Seller","Admin")]
    [HttpPost("Delete")]
    public async Task DeleteAsync([FromForm] DeleteGameDto deleteGameDto)
    {
        await _sender.Send(new DeleteCommand(deleteGameDto.GameId));
    }
    [HttpGet("GetGameByName")]
    public async Task<Game> GetGameByNameAsync(string gameName)
    {
       return await _sender.Send(new GetGameByNameQuery(gameName));
    }
    [HttpGet ("GetAllGames")]
    public async Task <IActionResult> GetAllGamesAsync()
    {
        var a = Json(await _sender.Send(new GetAllGamesQuery()));
        return a;
    }
    [HttpGet ("GetGameWithFilter")]
    public async Task <List<Game>> GetGameWithFiltersAsync ([FromQuery]GetFiltersDto getFilters)
    {
        return await _sender.Send(new GetGameWithFiltersQuery(getFilters.GameName, getFilters.Creator, getFilters.Genre, getFilters.Kind,  getFilters.PriceMaxValue, getFilters.PriceMinValue ,getFilters.IsDirectly));
    }

}

