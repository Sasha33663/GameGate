using Domain;

namespace Application.Common.Inteefaces;

public interface IGameRepository
{
    Task CreateAsync(Game game);

    Task DeleteGameAsync(string? gameId);

    Task<Game> GetGameByIdAsync(string? gameId);

    Task<Game> GetGameByNameAsync(string? gameName);

    Task<List<Game>> GetAllGamesAsync();

    Task<List<Game>> GetGamesWithFiltersAsync(string? gameName,
     string? creator,
     string? genre,
     string? kind,
     decimal? priceMaxValue,
     decimal? priceMinValue,
     bool? isDirectly); //TODO: объеденить параметры в класс 
}