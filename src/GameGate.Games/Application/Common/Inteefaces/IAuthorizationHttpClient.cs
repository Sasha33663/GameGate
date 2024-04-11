using Domain;

namespace Application.Common.Intefaces;

public interface IAuthorizationHttpClient
{
    Task<User> GetUserAsync(string coockie);
}