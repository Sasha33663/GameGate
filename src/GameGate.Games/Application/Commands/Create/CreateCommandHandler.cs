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
    public CreateCommandHandler(IGameRepository gameRepository, IAuthorizationHttpClient authorizationHttpClient)
    {
        _gameRepository = gameRepository;
        _authorizationHttpClient = authorizationHttpClient;
    } 

    public async Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var user = await _authorizationHttpClient.GetUserAsync(request.coockie);
        
        var newGame = new Game
        {
            GameName = request.Name,
            Description = request.Discription,
            GameId = Guid.NewGuid(),
            UserId = user.UserId,
            Genre = request.Genre,
            Kind = request.Kind,
            Creator = request.Creator,
        };
       await _gameRepository.CreateAsync(newGame);
        
    }
}



