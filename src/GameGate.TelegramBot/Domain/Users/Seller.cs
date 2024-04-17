using Domain.Games;

namespace Domain.Users;

public sealed class Seller : User
{
    public decimal Money { get; set; }
    public List<Game?> BoughtGames { get; set; }
    public List<Game?> SoldGames { get; set; }
}