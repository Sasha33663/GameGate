using Domain.Games;
using MediatR;

namespace Application.Queries.Games.GetAll;

public class GetAllGamesQuery : IRequest<List<Game>>
{
}