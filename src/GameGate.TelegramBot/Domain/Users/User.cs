using Domain.Games;

namespace Domain.Users;

public abstract class User
{
    public string UserName { get; set; }
    public string UserId { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public  List<Game> Games { get; set; }
}