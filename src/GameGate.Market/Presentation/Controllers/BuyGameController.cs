using Application.Commands;
using Application.Commands.MakeOrder;
using Application.Commands.SellGame;
using Application.Commands.SellGameDirectly;
using Application.Queries.Games.GetAll;
using Application.Queries.Games.GetWithFilters;
using Application.Queries.Orders;
using Domain;
using Domain.Games;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Buy;
using Presentation.Dto.Get;
using Presentation.Dto.Sell;

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
        try 
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

   
    [HttpPost("GetGames")]
    public async Task<List<Game?>> GetGameWithFiltersAsync([FromForm] GetGameWithFiltersDto gameDto)
    {
        return await _sender.Send(new GetGameWithFiltersQuery(gameDto.GameName, gameDto.Genre, gameDto.Creator,
        gameDto.Kind, gameDto.PriceMaxValue, gameDto.PriceMinValue, gameDto.IsDirectly));
    }

    [HttpPost("MakeOrder")]
    public async Task<IActionResult> MakeOrderAsync([FromForm] BuyGameDto buyGameDto)
    {
        var cookie = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key.StartsWith(".AspNetCore.Identity.Application"));
        var cookieString = $"{cookie.Key}={cookie.Value}";
        return Json(await _sender.Send(new MakeOrderCommand(buyGameDto.GameName, buyGameDto.Bid, cookieString)));
    }
    //[Authorize(Roles = "Seller")]
    [HttpGet("GetMyOrders")]
    public async Task <List<Order?>> GetOrdersAsync()
    {
        var cookie = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key.StartsWith(".AspNetCore.Identity.Application"));
        var cookieString = $"{cookie.Key}={cookie.Value}";
        return await _sender.Send(new GetMyOrdersQuery(cookieString));
    }
    //[Authorize(Roles = "Seller")]
    [HttpPost("SellGameByOrder")]
    public async Task SelllGameByOrderAsync([FromForm]SellGameByOrderDto sellGameDto)
    {
        await _sender.Send(new SellGameByOrderCommand(sellGameDto.OrderId)); 
    }
    [HttpPost("BuyDirectly")]
    public async Task BuyGameDirectlyAsync([FromForm]BuyGameDirectlyDto directlyDto)
    {
        var cookie = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key.StartsWith(".AspNetCore.Identity.Application"));
        var cookieString = $"{cookie.Key}={cookie.Value}";
        await _sender.Send(new BuyGameDirectlyCommand(directlyDto.GameName,directlyDto.Bid, cookieString));
    }  
}