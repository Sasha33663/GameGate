using Application.Common.Inteefaces;
using CloudinaryDotNet;
using Domain;
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
    private readonly IImageRepository _imageRepository;
    public DeleteCommandHandler (IGameRepository gameRepository, IImageRepository imageRepository)
    {
        _gameRepository = gameRepository;
        _imageRepository = imageRepository;
    }
    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
       var game =await _gameRepository.GetGameAsync(request.GameId);
       await _imageRepository.DeleteImageAsync(game.GamePreviewId);

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
