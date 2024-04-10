﻿using Application.Common.Inteefaces;
using MediatR;

namespace Application.Commands.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
{
    private readonly IGameRepository _gameRepository;
    private readonly IImageRepository _imageRepository;

    public DeleteCommandHandler(IGameRepository gameRepository, IImageRepository imageRepository)
    {
        _gameRepository = gameRepository;
        _imageRepository = imageRepository;
    }

    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetGameByIdAsync(request.GameId);
        await _imageRepository.DeleteImageAsync(game.GamePreviewId);
        await _gameRepository.DeleteGameAsync(request.GameId);
    }
}