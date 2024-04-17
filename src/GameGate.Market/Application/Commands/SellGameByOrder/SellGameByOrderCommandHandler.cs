using Application.Common.Interfaces;
using Domain.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SellGameByOrder;
public class SellGameByOrderCommandHandler : IRequestHandler<SellGameByOrderCommand>
{
    private readonly IMarketRepository _marketRepository;
    private readonly IGamesHttpClient _gamesHttpClient;
    public SellGameByOrderCommandHandler(IMarketRepository marketRepository, IGamesHttpClient gamesHttpClient)
    {
        _marketRepository = marketRepository;
        _gamesHttpClient = gamesHttpClient;
    }
    public async Task Handle(SellGameByOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _marketRepository.GetOrdersById(request.orderId);
        var buyer = _marketRepository.GetBuyerById(order.BuyerId);
        var seller = _marketRepository.GetSeller(order.SellerId);
        var game = await _gamesHttpClient.GetGameByNameAsync(order.GameName);
        if (order.GameName == null || buyer.Money <= 0 || buyer.Money < order.Bid || order.Bid < game.Price.PriceMinValue)
        {
            throw new Exception("Something wrong!");
        }
        game.RefaundDateTime = DateTime.UtcNow;
        seller.SoldGames?.Add(game);
        seller.Games?.Remove(game);
        buyer.BoughtGames.Add(game);
        var gain = buyer.Money - order.Bid;
        seller.Money += gain;
        await _gamesHttpClient.DeleteGameAsync(order.GameName);
        _marketRepository.DeleteOrder(request.orderId);
    }
}
