using Domain.Games;
using Domain.Orders;
using Domain.Users;

namespace Infrastructure.HttpClients;

public interface IMarketHttpClient
{
    Task<List<Game>> GetAllGamesAsync();
    Task<Game> GetGameByNameAsync(string gameName);
    Task DeleteGameAsync(string gameName);
    Task<List<Game>> GetGamesByAuthor(string authorId);
    Task<List<Order>> GetMyOrdersAsync(string Name);
}