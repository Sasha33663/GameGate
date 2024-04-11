using Application.Common.Intefaces;
using Domain;
using MediatR;

namespace Application.Queries.GetWithFIlters;

public class GetGameWithFiltersQueryHandler : IRequestHandler<GetGameWithFiltersQuery, List<Game>>
{
    private readonly IGameRepository _gameRepository;

    public GetGameWithFiltersQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<List<Game>> Handle(GetGameWithFiltersQuery request, CancellationToken cancellationToken)
    {
        return await _gameRepository.GetGamesWithFiltersAsync(request?.GameName, request?.Creator, request?.Genre, request?.Kind, request?.PriceMaxValue, request?.PriceMinValue, request?.IsDirectly);
    }
}