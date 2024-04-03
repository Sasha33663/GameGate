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
    private readonly IMarketHttpClient _marketHttpClient;
    public GetGameWithFiltersQueryHandler(/*IMarketRepository marketRepository,*/ IMarketHttpClient marketHttpClient)
    {
        //_marketRepository = marketRepository;
        _marketHttpClient = marketHttpClient;
    }
    public async Task<List<Game>> Handle(GetGameWithFiltersQuery request, CancellationToken cancellationToken)
    {
        var games = await _marketHttpClient.GetAllGamesAsync();
        var filteredGame = new FilteredGameDto
        {
            GameName = request.GameName,
            Creator = request.Creator,
            Genre = request.Genre,
            Kind = request.Kind,
            PriceMaxValue = request.PriceMaxValue,
            PriceMinValue = request.PriceMinValue,
            IsDirectly = request.IsDirectly,
        };
        throw new NotImplementedException();


    }


}
