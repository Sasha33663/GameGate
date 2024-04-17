using Application.Common.Interfaces;
using Domain.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Refund;
public class RefundCommandHandler : IRequestHandler<RefundCommand>
{
    private readonly IAuthHttpClient _authHttpClient;
    private readonly IMarketRepository _marketRepository;
    public RefundCommandHandler(IAuthHttpClient authHttpClient,IMarketRepository marketRepository)
    {
        _authHttpClient = authHttpClient;
        _marketRepository = marketRepository;
    }

    public async Task Handle(RefundCommand request, CancellationToken cancellationToken)
    {
        var userBuyer = await _authHttpClient.GetBuyerByCookieAsync(request.Cookie);
        var buyer = _marketRepository.GetBuyer(userBuyer.UserId);
        var game = buyer.BoughtGames.FirstOrDefault(x => x.GameName == request.GameName);
        if (game.RefaundDateTime==DateTime.UtcNow.AddHours(2))
        {
            throw new Exception("The money for the game can only be refunded within two hours of purchase");
        }
        var seller = await _authHttpClient.GetSellerByIdAsync(game.AuthorId);
        seller.Money -=game.Price.PriceMinValue;
        buyer.Money += game.Price.PriceMinValue;
        buyer.BoughtGames.Remove(game);
        seller.Games.Add(game);
    }
}
