using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Inteefaces;
public interface IGameRepository
{
    Task CreateAsync(Game game);
    Task DeleteGameByNameAsync (string? gameName);
    Task DeleteGameByIdAsync(string? gameId);
    Task<Game> GetGameAsync(string? gameId);
}
