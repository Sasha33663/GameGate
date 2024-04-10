using Application.Common.Interfaces;
using Application.Queries.GetWithFilters.Dto;
using Domain.Games;
using MediatR;

namespace Application.Queries.GetWithFilters;

public class GetGameWithFiltersQueryHandler : IRequestHandler<GetGameWithFiltersQuery, List<Game>>
{
    //private readonly IMarketRepository _marketRepository;  //TODO: удалить комментарий
    private readonly IGamesHttpClient _gamesHttpClient;

    public GetGameWithFiltersQueryHandler(/*IMarketRepository marketRepository,*/ IGamesHttpClient gamesHttpClient)  //TODO: удалить комментарий
    {
        //_marketRepository = marketRepository;  //TODO: удалить комментарий
        _gamesHttpClient = gamesHttpClient;
    }

    public async Task<List<Game>> Handle(GetGameWithFiltersQuery request, CancellationToken cancellationToken)
    {
        var filteredGame = new FilteredGameDto
        {
            GameName = request?.GameName,
            Creator = request?.Creator,
            Genre = request?.Genre,
            Kind = request?.Kind,
            PriceMaxValue = request?.PriceMaxValue,
            PriceMinValue = request?.PriceMinValue,
            IsDirectly = request?.IsDirectly,
        };

        var a = await _gamesHttpClient.GetGamesWithFiltersAsync(filteredGame);
        return a;
    }
}