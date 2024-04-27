using Domain;
using Infrastructure.HttpClients;
using Infrastructure.Repository;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ReactionService;
public class ReactionService : IReactionService
{
    private readonly IReactionRepository _reactionRepository;
    private readonly IAuthorizationHttpClient _authorizationHttpClient;

    public ReactionService(IReactionRepository reactionRepository, IAuthorizationHttpClient authorizationHttpClient)
    {
        _reactionRepository = reactionRepository;
        _authorizationHttpClient = authorizationHttpClient;
    }
    public async Task<Reaction> CreateReactionAsync(bool withoutReview, int stars, string? reviews, string cookie, string gameName)
    {
        var user = await _authorizationHttpClient.GetUserAsync(cookie);
       
        var newReaction = new Reaction
        {

            AuthorId = user.UserId,
            AuthorName = user.UserName,
            ReactionId = Guid.NewGuid(),
            GameName = gameName,
            Reviews = reviews,
            Stars = stars
        };

        await _reactionRepository.CreateReactionAsync(newReaction);
        return newReaction;
    }
}
