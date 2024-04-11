using Domain.Users;

namespace Application.Common.Interfaces;

public interface IAuthHttpClient
{
    Task<Buyer> GetBuyerByCookieAsync(string cookie);
    Task<Seller> GetSellerByCookieAsync(string cookie);
    Task<Seller> GetSellerByIdAsync(string userId);
}