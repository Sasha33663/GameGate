using Domain.Orders;
using Domain.Users;

namespace Application.Common.Interfaces;

public interface IMarketRepository
{
    Task<Buyer> CreateBuyerAsync(Buyer user);

    Task<Order> CreateOrderAsync(Order order);

   Task < List<Order?>> GetOrdersByName(string sellerName);
    Order GetOrdersById(Guid orderId);
    Buyer GetBuyer(string userId);
    void DeleteOrder(Guid orderId);
    Buyer GetBuyerById(string buyerId);
    Seller GetSeller(string authorId);
    Task CreateSellerAsync(Seller user);
}