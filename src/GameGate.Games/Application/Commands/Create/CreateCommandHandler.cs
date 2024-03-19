using Application.Common.Inteefaces;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Create;
public class CreateCommandHandler : IRequestHandler<CreateCommand>
{
    private readonly IGameRepository _gameRepository;
    private readonly IAuthorizationHttpClient _authorizationHttpClient;
    private readonly IImageRepository _imageRepository;
    public CreateCommandHandler(IGameRepository gameRepository, IAuthorizationHttpClient authorizationHttpClient, IImageRepository imageRepository)
    {
        _gameRepository = gameRepository;
        _authorizationHttpClient = authorizationHttpClient;
        _imageRepository = imageRepository;
    }

    public async Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var user = await _authorizationHttpClient.GetUserAsync(request.coockie);

        (var Id, var Url) = await _imageRepository.UploadImageAsync(request.GamePreviewName, request.GamePreview);

        var newGame = new Game
        {
            GameName = request.Name,
            Description = request.Discription,
            GamePreviewUrl = Url,
            GamePreviewId = Id,
            GameId = Guid.NewGuid(),
            UserId = user.UserId,
            Genre = request.Genre,
            Kind = request.Kind,
            Creator = request.Creator,
        };
        await _gameRepository.CreateAsync(newGame);

    }
}



