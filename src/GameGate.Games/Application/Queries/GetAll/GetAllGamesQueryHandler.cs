using Application.Common.Inteefaces;
using Domain;
using MediatR;

namespace Application.Queries.GetAll;

public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, List<Game>>
{
    private readonly IGameRepository _gameRepository;

    public GetAllGamesQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<List<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        return await _gameRepository.GetAllGamesAsync();
    }
}