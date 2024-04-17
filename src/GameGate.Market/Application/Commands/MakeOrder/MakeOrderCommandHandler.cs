using Application.Common.Interfaces;
using Domain.Orders;
using Domain.Users;
using MediatR;

namespace Application.Commands.MakeOrder;

public class MakeOrderCommandHandler : IRequestHandler<MakeOrderCommand, Order>
{
    private readonly IMarketRepository _marketRepository;
    private readonly IGamesHttpClient _gamesHttpClient;
    private readonly IAuthHttpClient _authHttpClient;

    public MakeOrderCommandHandler(IMarketRepository marketRepository, IGamesHttpClient gamesHttpClient, IAuthHttpClient authHttpClient)
    {
        _marketRepository = marketRepository;
        _gamesHttpClient = gamesHttpClient;
        _authHttpClient = authHttpClient;
    }

    public async Task<Order> Handle(MakeOrderCommand request, CancellationToken cancellationToken)
    {
        var userTask = _authHttpClient.GetBuyerByCookieAsync(request.cookie);
        var gameTask =  _gamesHttpClient.GetGameByNameAsync(request.GameName);
        await Task.WhenAll(userTask, gameTask);
        var user= userTask.Result;
        var game = gameTask.Result;
        var findSellerTask = _marketRepository.GetSeller(game.AuthorId);
        var findBuyerTask  =  _marketRepository.GetBuyer(user.UserId);
        if (findSellerTask == null)
        {
           var defaultSeller = await _authHttpClient.GetSellerByIdAsync(game.AuthorId);
           var seller = new Seller
            {
               BoughtGames = [],
               Email= defaultSeller.Email,
               Games =await  _gamesHttpClient.GetGamesByAuthor(game.AuthorId),
               Money=10000,
               PhoneNumber=defaultSeller.PhoneNumber,
               SoldGames=defaultSeller.SoldGames,
               Role=defaultSeller.Role,
               UserId=defaultSeller.UserId,
               UserName=defaultSeller.UserName               
            };
            await _marketRepository.CreateSellerAsync(seller);
        }
        if (findBuyerTask == null)
        {
            await _marketRepository.CreateBuyerAsync(user);
        }
        var buyer = _marketRepository.GetBuyer(user.UserId);
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            GameName = game.GameName,
            GameId = game.GameId,
            Bid = request.Bid,
            BuyerName = buyer.UserName,
            BuyerId = buyer.UserId,
            Cost = $"{game.Price.PriceMinValue} - {game.Price.PriceMaxValue}",
            DateTime = DateTime.UtcNow.Date,
            SellerName = game.AuthorName,
            SellerId = game.AuthorId

        };
        return await _marketRepository.CreateOrderAsync(order);
    }
}