using Domain;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;
public interface IMarketRepository
{
    Task <Buyer>CreateBuyerAsync(Buyer user);
    Task <Order> CreateOrderAsync(Order order);
    List<Order> GetOrders(string sellerName);
    Buyer GetBuyer(string userId);
}
