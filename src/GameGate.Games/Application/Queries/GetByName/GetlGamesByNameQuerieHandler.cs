using Application.Common.Inteefaces;
using Domain;
using MediatR;

namespace Application.Queries.GetByName;

public class GetGameByNameQueryHandler : IRequestHandler<GetGameByNameQuery, Game>
{
    private readonly IGameRepository _gameRepository;

    public GetGameByNameQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Game> Handle(GetGameByNameQuery request, CancellationToken cancellationToken)
    {
        return await _gameRepository.GetGameByNameAsync(request.gameName);
    }
}