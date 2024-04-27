using Domain;

namespace Infrastructure.HttpClients;

public interface IAuthorizationHttpClient
{
    Task<User> GetUserAsync(string coockie);
}