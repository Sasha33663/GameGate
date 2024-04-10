using Domain;
using Domain.Users;

namespace Application.Common.Interfaces;

public interface IMarketRepository
{
    Task<Buyer> CreateBuyerAsync(Buyer user);

    Task<Order> CreateOrderAsync(Order order);

    List<Order> GetOrders(string sellerName);

    Buyer GetBuyer(string userId);
}