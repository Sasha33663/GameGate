using Application.Common.Interfaces;
using Domain.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAll;
public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, List<Game>>
{
    private readonly IMarketHttpClient _marketHttpClient;
    public GetAllGamesQueryHandler(IMarketHttpClient marketHttpClient)
    {
        _marketHttpClient = marketHttpClient;
    }
    public async Task<List<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        return await _marketHttpClient.GetAllGamesAsync();
    }
}
