using Application.Queries.GetAll;
using Application.Queries.GetWithFilters;
using Domain.Games;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
        var game =  await _sender.Send(new GetAllGamesQuery());
        try
        {
            var result =game.Select(x => new GetGameDto
            {
                GameName = x.GameName,
                Description = x.Description,
                GamePreviewUrl = x.GamePreviewUrl,
                Creator= x.Filters.Creator,
                Genre= x.Filters.Genre,
                Kind = x.Filters.Kind,
                IsDirectly = x.Price.IsDirectly,
                PriceMaxValue=x.Price.PriceMaxValue,
                PriceMinValue=x.Price.PriceMinValue,
                
            });
            return result.ToList();
        }   
        catch  (Exception ex) 
        {
            throw ex ;
        }
         
    }
    [HttpPost("GetGames")]
    public async Task<List<Game?>> GetGameWithFiltersAsync([FromForm]GetGameWithFiltersDto gameDto)
    {
           var a =await _sender.Send(new GetGameWithFiltersQuery(gameDto.GameName, gameDto.Genre, gameDto.Creator,
           gameDto.Kind, gameDto.PriceMaxValue, gameDto.PriceMinValue, gameDto.IsDirectly));
        return a;
     
    }
}
