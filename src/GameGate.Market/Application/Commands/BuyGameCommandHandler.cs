using Application.Common.Interfaces;
using Domain;
using MediatR;
using Presentation.Controllers;
using System.Security.Cryptography.X509Certificates;

namespace Application.Commands;
public class BuyGameCommandHandler : IRequestHandler<BuyGameCommand, Order>
{
    private readonly IMarketRepository _marketRepository;
    private readonly IGamesHttpClient _gamesHttpClient;
    public BuyGameCommandHandler(IMarketRepository marketRepository, IGamesHttpClient gamesHttpClient)
    {
        _marketRepository = marketRepository;
        _gamesHttpClient = gamesHttpClient;
    }
    public async Task<Order> Handle(BuyGameCommand request, CancellationToken cancellationToken)
    {
        var user = await _gamesHttpClient.GetUserAsync(request.cookie);
        var game = await _gamesHttpClient.GetGameByNameAsync(request.GameName);
        var buyer = _marketRepository.GetBuyer(user.UserId);
        if (buyer == null)
        {
           await _marketRepository.CreateBuyerAsync(user);

        }

        var order = new Order
        { 
            OrderId = Guid.NewGuid(),
            GameName=game.GameName,
            Buyer = _marketRepository.GetBuyer(user.UserId),
            Bid =request.Bid,
            Cost=$"{game.Price.PriceMinValue} - {game.Price.PriceMaxValue}",
            DateTime = DateTime.UtcNow.Date,
            IsMade = false,
            Seller = game.Author
        };
        return await _marketRepository.CreateOrderAsync(order);
    }
}
