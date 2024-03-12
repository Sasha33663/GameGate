using Application.Common.Inteefaces;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Create;
public class CreateCommandHandler :IRequestHandler<CreateCommand>
{
    private readonly IGameRepository _gameRepository;
    private readonly IAuthorizationHttpClient _authorizationHttpClient;
    public CreateCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    } 

    public Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var newGame = new Game
        {
            Name = request.Name,
            Description = request.Discription,
            GameId = Guid.NewGuid(),
            UserId = ConverTo.(_authorizationHttpClient.GetUserAsync()),
        Genre = request.Genre,
            Kind = request.Kind,
            Creator = request.Creator,
        };
        return (_gameRepository.CreateAsync(newGame));
    }
}



