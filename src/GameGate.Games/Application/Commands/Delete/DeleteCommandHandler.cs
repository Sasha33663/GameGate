using Application.Common.Inteefaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Delete;
public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
{
    private readonly IGameRepository _gameRepository;
    public DeleteCommandHandler (IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        await _gameRepository.GetGameAsync(request.GameId);
        if (request.GameName==null && request.GameId.Length>0)
        {
             await _gameRepository.DeleteGameByIdAsync(request.GameId);
        }
        else if (request.GameId==null && request.GameName.Length>0)
        {
             await _gameRepository.DeleteGameByNameAsync(request.GameName);
        }
        else if(request.GameName==null && request.GameId == null)
        {
            throw new Exception("The Id or Name must be filled in");
        }
        else if (request.GameName.Length>0 && request.GameId.Length > 0)
        {
            await _gameRepository.DeleteGameByNameAsync(request.GameName);
        }
    }
}
