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

    public List<Order?> GetOrdersByName(string sellerName)
    {
        return _marketDatabase.Orders.Where(x => x.SellerName == sellerName).ToList();
    }

    public void DeleteOrder(Guid orderId)
    {
        var order = _marketDatabase.Orders.FirstOrDefault(x => x.OrderId == orderId);
        _marketDatabase.Orders.Remove(order);
        _marketDatabase.SaveChanges();
    }

    public Order GetOrdersById(Guid orderId)
    {
        return  _marketDatabase.Orders.FirstOrDefault(x => x.OrderId == orderId);
    }

    public Buyer GetBuyerById(string buyerId)
    {
        var user= _marketDatabase.Buyers.FirstOrDefault(x => x.UserId == buyerId);
        var buyer =new Buyer
        {
            UserId = user.UserId,
            BoughtGames = user.BoughtGames,
            Money = user.Money,
            UserName = user.UserName,
            Email = user.Email,
            Games = user.Games,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            
        };
        return buyer;
    }
    public Task CreateSellerAsync(Seller user)
    {
        _marketDatabase.Sellers.Add(user);
        return Task.CompletedTask;
    }
    public Seller GetSeller(string authorId)
    {
        return _marketDatabase.Sellers.FirstOrDefault(x => x.UserId == authorId);
    }

   
}