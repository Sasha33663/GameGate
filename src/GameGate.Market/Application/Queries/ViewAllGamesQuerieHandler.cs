using Application.Common.Interfaces;
using Domain.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries;
public class ViewAllGamesQuerieHandler : IRequestHandler<ViewAllGamesQuerie, List<Game>>
{
    //private readonly IMarketRepository _marketRepository;
    private readonly IMarketHttpClient _marketHttpClient;
    public ViewAllGamesQuerieHandler(/*IMarketRepository marketRepository,*/ IMarketHttpClient marketHttpClient)
    {
        //_marketRepository = marketRepository;
        _marketHttpClient = marketHttpClient;
    }
    public async Task<List<Game>> Handle(ViewAllGamesQuerie request, CancellationToken cancellationToken)
    {
        return await _marketHttpClient.GetAllGamesAsync();
    }
}
