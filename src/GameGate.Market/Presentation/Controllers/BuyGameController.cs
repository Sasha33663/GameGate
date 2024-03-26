using Application.Queries;
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
[Route("api/market")]
public class BuyGameController : Controller
{
    private readonly ISender _sender;
    public BuyGameController(ISender sender)
    {
        _sender = sender;
    }
    [HttpPost("ViewAllGames")]
    public async Task<List<GameDto>> ViewAllGamesAsync()
    {
        var game =  await _sender.Send(new ViewAllGamesQuerie());
        try
        {
            var result =game.Select(x => new GameDto
            {
                GameName = x.GameName,
                //Description = x.Description,
            });
            return result.ToList();
        }   
        catch  (Exception ex) 
        {
            throw ex ;
        }
         
    }
    
}
