using Application.Queries.GetWithFilters.Dto;
using Domain.Games;
using Domain.Users;

namespace Application.Common.Interfaces;

public interface IGamesHttpClient
{
    Task<List<Game>> GetAllGamesAsync();

    Task<Game> GetGameByNameAsync(string gameName);

    Task<List<Game>> GetGamesWithFiltersAsync(FilteredGameDto filteredGameDto);

    Task<Buyer> GetUserAsync(string coockie);
}