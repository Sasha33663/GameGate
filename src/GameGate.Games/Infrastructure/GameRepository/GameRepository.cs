using Application.Common.Inteefaces;
using CloudinaryDotNet;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GameRepository;
public class GameRepository : IGameRepository
{
    private readonly Database _gameDatabase;
    private readonly Cloudinary
    public GameRepository (Database database)
    {
        _gameDatabase = database;
    }
    public async Task CreateAsync(Game game)
    {
      await _gameDatabase.Games.AddAsync(game);
       _gameDatabase.SaveChanges();
    }
    public async Task DeleteGameByNameAsync (string? gameName)
    {
        var result= await _gameDatabase.Games.FirstOrDefaultAsync(x=>x.GameName == gameName);
         _gameDatabase.Remove(result);
        _gameDatabase.SaveChanges();

    }
    public async Task DeleteGameByIdAsync(string? gameId)
    {
        
        var result = await _gameDatabase.Games.FirstOrDefaultAsync(x => x.GameId.ToString() == gameId);
        _gameDatabase.Remove(result);
    }
    public async Task <Game> GetGameAsync (string? gameId)
    {
        var result = await _gameDatabase.Games.FirstOrDefaultAsync(x => x.GameId.ToString() == gameId);
        if (result == null)
        {
            throw new Exception("The game was not found");
        }
        return result;
    }

}
