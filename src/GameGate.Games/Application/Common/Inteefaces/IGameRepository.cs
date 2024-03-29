﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Inteefaces;
public interface IGameRepository
{
    Task CreateAsync(Game game);
    Task DeleteGameAsync(string? gameId);
    Task<Game> GetGameByIdAsync(string? gameId);
    Task<Game> GetGameByNameAsync(string? gameName);

    Task <List<Game>> GetAllGamesAsync ();

}
