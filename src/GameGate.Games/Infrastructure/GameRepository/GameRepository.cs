using Application.Common.Inteefaces;
using Domain;
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
    public GameRepository (Database database)
    {
        _gameDatabase = database;
    }
    public async Task CreateAsync(Game game)
    {
     var a= await _gameDatabase.Games.AddAsync(game);
     var b=  _gameDatabase.SaveChanges();
    }
    public async Task GetGameAsync (Game game)
    {
        await _gameDatabase.();
     }
    public async Task DeleteAsync(Game game)
    {
       _gameDatabase.Remove(game);
       await _gameDatabase.SaveChangesAsync();
    }

}
