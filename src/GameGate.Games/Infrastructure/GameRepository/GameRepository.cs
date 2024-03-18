using Application.Common.Inteefaces;
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
    private readonly DbSet <Game> _gameSet;
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
        var result= await _gameSet.FirstOrDefaultAsync(x=>x.GameName == gameName);
         _gameSet.Remove(result);
    }
    public async Task DeleteGameByIdAsync(string? gameId)
    {
        
        var result = await _gameSet.FirstOrDefaultAsync(x => x.GameId.ToString() == gameId);
        _gameSet.Remove(result);
    }

}
