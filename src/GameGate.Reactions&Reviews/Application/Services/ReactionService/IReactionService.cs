using Domain;

namespace Presentation.Controllers;

public interface IReactionService
{
    Task <Reaction> CreateReactionAsync(bool withoutReview, int stars, string? reviews, string cookie, string GameName);
}