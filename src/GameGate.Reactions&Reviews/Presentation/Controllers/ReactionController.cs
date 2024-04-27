using Application.Services.ReactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Presentation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers;
[ApiController]
[Route("api/reaction")]
public class ReactionController :Controller
{
    private readonly IReactionService _reaction;
    //[OutputCache(Duration =30)]
    //[HttpPost("CreateReaction")]
    //public async Task <IActionResult> CreateReactionAsync([FromForm]ReactionDto reactionDto)
    //{
    //    var cookieString = GetCookie();
    //    return Json (await _reaction.CreateReactionAsync(reactionDto?.WithoutReview,reactionDto?.Stars,reactionDto?.Reviews, cookieString,reactionDto.GameName));
    //}
    [HttpGet]
    [OutputCache(Duration =360)]
    public async Task <IActionResult> GetReactionsAsync()
    {

    }
    private string GetCookie()
    {
        var cookie = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key.StartsWith(".AspNetCore.Identity.Application"));
        var cookieString = $"{cookie.Key}={cookie.Value}";
        return cookieString;
    }
}
