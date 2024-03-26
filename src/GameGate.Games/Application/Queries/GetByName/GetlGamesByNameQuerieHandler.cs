using Application.Common.Inteefaces;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Get;
public class GetGameByNameQuerieHandler : IRequestHandler<GetGameByNameQuerie, Game>
{
    private readonly IGameRepository _gameRepository;
    public GetGameByNameQuerieHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    public async Task<Game> Handle(GetGameByNameQuerie request, CancellationToken cancellationToken)
    {
        return await _gameRepository.GetGameByNameAsync(request.gameName);
    }
}
