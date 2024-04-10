using Application.Common.Interfaces;
using Domain;
using Domain.Games;
using Domain.Users;

namespace Infrastructure.MarketRepository;

public class MarketRepository : IMarketRepository
{
    private readonly Database _marketDatabase;

    public MarketRepository(Database database)
    {
        _marketDatabase = database;
    }

    public async Task<Buyer> CreateBuyerAsync(Buyer user)
    {
        var buyer = new Buyer
        {
            BoughtGames = new List<string>(),
            Money = 10000,
            UserName = user.UserName,
            Email = user.Email,
            Games = new List<Game>(),
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            UserId = user.UserId,
        };
        await _marketDatabase.AddAsync(buyer);
        await _marketDatabase.SaveChangesAsync();
        return buyer;
    }

    public Buyer GetBuyer(string userId)
    {
        return _marketDatabase.Buyers.FirstOrDefault(x => x.UserId == userId);
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _marketDatabase.AddAsync(order);
        await _marketDatabase.SaveChangesAsync();
        return order;
    }

    public List<Order> GetOrders(string sellerName)
    {
        return _marketDatabase.Orders.Where(x => x.Seller == sellerName).ToList();
    }
}