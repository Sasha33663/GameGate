using Application.Common.Inteefaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.GameRepository;

public class GameRepository : IGameRepository
{
    private readonly Database _gameDatabase;

    public GameRepository(Database database)
    {
        _gameDatabase = database;
    }

    public async Task CreateAsync(Game game)
    {
        await _gameDatabase.Games.AddAsync(game);
        _gameDatabase.SaveChanges();
    }

    public async Task DeleteGameAsync(string? gameId)
    {
        var result = await _gameDatabase.Games.FirstOrDefaultAsync(x => x.GameId.ToString() == gameId);
        _gameDatabase.Remove(result);
        _gameDatabase.SaveChanges();
    }

    public async Task<Game> GetGameByIdAsync(string? gameId)
    {
        var result = await _gameDatabase.Games.FirstOrDefaultAsync(x => x.GameId.ToString() == gameId);
        if (result == null)
        {
            throw new Exception("The game was not found");
        }
        return result;
    }

    public async Task<Game> GetGameByNameAsync(string? gameName)
    {
        return await _gameDatabase.Games.FirstOrDefaultAsync(x => x.GameName == gameName);
    }

    public async Task<List<Game>> GetAllGamesAsync()
    {
        List<Game> result = _gameDatabase.Games.ToList();
        return result;
    }

    public Task<List<Game>> GetGamesWithFiltersAsync(string? gameName, string? creator, string? genre, string? kind, decimal? priceMaxValue, decimal? priceMinValue, bool? isDirectly)
    {
        var query = _gameDatabase.Games.AsQueryable();
        if (gameName != null)
        {
            query = query.Where(x => x.GameName == gameName);
        }
        if (creator != null)
        {
            query = query.Where(x => x.Filters.Creator == creator);
        }
        if (genre != null)
        {
            query = query.Where(x => x.Filters.Genre == genre);
        }
        if (kind != null)
        {
            query = query.Where(x => x.Filters.Kind == kind);
        }
        if (priceMaxValue != null)
        {
            query = query.Where(x => x.Price.PriceMaxValue >= priceMinValue || x.Price.PriceMaxValue > 0);
        }
        if (priceMinValue != null)
        {
            query = query.Where(x => x.Price.PriceMinValue == priceMinValue | x.Price.PriceMinValue <= priceMaxValue);
        }

        return query.ToListAsync();
    }
}