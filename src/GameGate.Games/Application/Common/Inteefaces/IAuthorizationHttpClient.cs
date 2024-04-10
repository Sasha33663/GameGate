using Domain;

namespace Application.Common.Inteefaces; //TODO: исправить название папки на Interfaces

public interface IAuthorizationHttpClient
{
    Task<User> GetUserAsync(string coockie);
}