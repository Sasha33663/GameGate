namespace Domain.Users;

public sealed class Buyer : User
{
    public List<string?> BoughtGames { get; set; }
    public decimal Money { get; set; }
}