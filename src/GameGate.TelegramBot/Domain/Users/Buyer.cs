using Domain.Games;

namespace Domain.Users;

public sealed class Buyer : User
{
    public List<Game?> BoughtGames { get; set; }
    public decimal Money { get; set; }
}