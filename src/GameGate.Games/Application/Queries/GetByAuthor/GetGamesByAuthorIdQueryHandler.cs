using Application.Common.Intefaces;
using Domain;
using MediatR;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetByAuthor;
public class GetGamesByAuthorIdQueryHandler : IRequestHandler<GetGamesByAuthorIdQuery, List<Game>>
{
    private readonly IGameRepository _gameRepository;

    public GetGamesByAuthorIdQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<List<Game>> Handle(GetGamesByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        return _gameRepository.GetGamesById(request.authorId);
    }
}
