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
      await _gameDatabase.Games.AddAsync(game);
      await _gameDatabase.SaveChangesAsync();
    }

}
