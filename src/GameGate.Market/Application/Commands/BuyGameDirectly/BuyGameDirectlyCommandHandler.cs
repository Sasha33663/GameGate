using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SellGameDirectly;
public class BuyGameDirectlyCommandHandler : IRequestHandler<BuyGameDirectlyCommand>
{
    private readonly IMarketRepository _marketRepository;
    private readonly IGamesHttpClient _gamesHttpClient;
    private readonly IAuthHttpClient _authHttpClient;

    public BuyGameDirectlyCommandHandler(IMarketRepository marketRepository, IGamesHttpClient gamesHttpClient, IAuthHttpClient authHttpClient)
    {
        _marketRepository = marketRepository;
        _gamesHttpClient = gamesHttpClient;
        _authHttpClient = authHttpClient;
    }

    public async Task Handle(BuyGameDirectlyCommand request, CancellationToken cancellationToken)
    {
        var game = await _gamesHttpClient.GetGameByNameAsync(request.GameName);
        if(game.Price.IsDirectly ==false)
        {
            throw new Exception("You can't buy this game directly, make an order");
        }
        var seller = _marketRepository.GetSeller(game.AuthorId);
        var user = await _authHttpClient.GetBuyerByCookieAsync(request.cookie);
        var buyer = _marketRepository.GetBuyer(user.UserId);
        if (buyer.Money < game.Price.PriceMinValue)
        {
            throw new Exception("There are not enough funds on the balance sheet");
        }
        seller.SoldGames?.Add(game);
        seller.Games?.Remove(game);
        var gain = buyer.Money - request.Bid;
        seller.Money += gain;
        await _gamesHttpClient.DeleteGameAsync(game.GameName);
    }
}
