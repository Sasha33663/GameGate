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
    Task GetGameAsync(Game game);
    Task DeleteAsync(Game game);
}
