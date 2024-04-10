using Application.Queries.GetWithFilters.Dto;
using Domain.Games;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;
public interface IGamesHttpClient
{
    Task<List<Game>> GetAllGamesAsync();
    Task <Game> GetGameByNameAsync(string gameName);
    Task<List<Game>> GetGamesWithFiltersAsync(FilteredGameDto filteredGameDto);
    Task <Buyer>GetUserAsync(string coockie);
}
