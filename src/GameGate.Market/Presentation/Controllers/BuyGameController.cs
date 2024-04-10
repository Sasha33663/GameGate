using Application.Queries.GetAll;
using Application.Queries.GetWithFilters;
using Domain.Games;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;

namespace Presentation.Controllers;

[ApiController]
[Route("api/market/buy")]
public class BuyGameController : Controller
{
    private readonly ISender _sender;

    public BuyGameController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("ViewAllGames")]
    public async Task<List<GetGameDto>> ViewAllGamesAsync()
    {
        var game = await _sender.Send(new GetAllGamesQuery());
        try //TODO: убрать try...catch
        {
            var result = game.Select(x => new GetGameDto
            {
                GameName = x.GameName,
                Description = x.Description,
                GamePreviewUrl = x.GamePreviewUrl,
                Creator = x.Filters.Creator,
                Genre = x.Filters.Genre,
                Kind = x.Filters.Kind,
                IsDirectly = x.Price.IsDirectly,
                PriceMaxValue = x.Price.PriceMaxValue,
                PriceMinValue = x.Price.PriceMinValue,
            });
            return result.ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //TODO: Эта апишка - то же самое, что и api/games/getgamewithfilter - значит она не нужна?
    [HttpPost("GetGames")]
    public async Task<List<Game?>> GetGameWithFiltersAsync([FromForm] GetGameWithFiltersDto gameDto)
    {
        return await _sender.Send(new GetGameWithFiltersQuery(gameDto.GameName, gameDto.Genre, gameDto.Creator,
        gameDto.Kind, gameDto.PriceMaxValue, gameDto.PriceMinValue, gameDto.IsDirectly));
    }

    [HttpPost("MakeOrder")]
    public async Task<IActionResult> MakeOrderAsync([FromBody] BuyGameDto buyGameDto)
    {
        var cookie = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key.StartsWith(".AspNetCore.Identity.Application"));
        var cookieString = $"{cookie.Key}={cookie.Value}";
        return Json(await _sender.Send(new BuyGameCommand(buyGameDto.GameName, buyGameDto.Bid, cookieString)));
    }
}