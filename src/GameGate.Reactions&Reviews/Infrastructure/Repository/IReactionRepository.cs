
using Domain;

namespace Infrastructure.Repository;

public interface IReactionRepository
{
    Task <Reaction> CreateReactionAsync(Reaction reaction);
}