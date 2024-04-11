using Domain.Users;

namespace Domain;

public sealed class Order
{
    public Guid OrderId { get; set; }
    public string GameName { get; set; }
    public string GameId { get; set; }
    public string BuyerName { get; set; }
    public string BuyerId { get; set; }
    public decimal Bid { get; set; }
    public string Cost { get; set; }
    public string SellerName { get; set; }
    public string SellerId { get; set; }
    public DateTime DateTime { get; set; }
}