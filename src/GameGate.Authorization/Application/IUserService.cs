using Domain;

namespace Application;

public interface IUserService
{
    Task UserRegisterAsync(string userName, string password, CancellationToken cancellationToken);

    Task UserLogInAsync(string userName, string password, CancellationToken cancellationToken);

    Task UserDeleteAsync(string userName, string password, CancellationToken cancellationToken);

    Task MakeRoleAsync(string roleName);

    Task GiveRoleAsync(Guid userId, string roleName);

    Task<User> GetUserAsync(string userName);
}