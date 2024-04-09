using Application.Common.Interfaces;
using Application.Queries.GetWithFilters.Dto;
using Domain.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetWithFilters;
public class GetGameWithFiltersQueryHandler : IRequestHandler<GetGameWithFiltersQuery, List<Game>>
{
    //private readonly IMarketRepository _marketRepository;
    private readonly IGamesHttpClient _gamesHttpClient;
    public GetGameWithFiltersQueryHandler(/*IMarketRepository marketRepository,*/ IGamesHttpClient gamesHttpClient)
    {
        //_marketRepository = marketRepository;
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
