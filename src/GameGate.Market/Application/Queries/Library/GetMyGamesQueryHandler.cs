using Application.Common.Interfaces;
using Domain.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Library;
public class GetMyGamesQueryHandler : IRequestHandler<GetMyGamesQuery, List<Game?>>
{
    private readonly IAuthHttpClient _authHttpClient;
    private readonly IMarketRepository _marketRepository;

    public GetMyGamesQueryHandler(IAuthHttpClient authHttpClient,IMarketRepository marketRepository)
    {
        _marketRepository = marketRepository;
        _authHttpClient = authHttpClient;
    }
    public async Task<List<Game?>> Handle(GetMyGamesQuery request, CancellationToken cancellationToken)
    {
        var user = await _authHttpClient.GetBuyerByCookieAsync(request.Cookie);
        var buyer= _marketRepository.GetBuyerById(user.UserId);
        return buyer.BoughtGames;
    }
}
