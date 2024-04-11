using Application.Common.Interfaces;
using MediatR;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SellGame;
public class SellGameCommandHandler : IRequestHandler<SellGameCommand>
{
    private readonly IMarketRepository _marketRepository;
    private readonly IGamesHttpClient _gamesHttpClient;
    public SellGameCommandHandler(IMarketRepository marketRepository,IGamesHttpClient gamesHttpClient)
    {
        _marketRepository = marketRepository;
        _gamesHttpClient = gamesHttpClient;
    }
    public async Task Handle(SellGameCommand request, CancellationToken cancellationToken)
    {
        var order= _marketRepository.GetOrdersById(request.orderId);
        var buyer=   _marketRepository.GetBuyerById(order.BuyerId);
        var seller=   _marketRepository.GetSeller(order.SellerId);
        var game =await  _gamesHttpClient.GetGameByNameAsync(order.GameName);
        //seller.Games.Remove(game);
        //seller.SoldGames.Add(game);
        var gain = buyer.Money - order.Bid;
        seller.Money += gain;
        _gamesHttpClient.DeleteGameAsync(order.GameName);
        _marketRepository.DeleteOrder(request.orderId);
    }
}
