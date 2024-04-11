using Application.Common.Intefaces;
using MediatR;

namespace Application.Commands.Delete.ById;

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
        var deletImg = _imageRepository.DeleteImageAsync(game.GamePreviewId);
        var deletGame = _gameRepository.DeleteGameAsync(request.GameId);
        await Task.WhenAll(deletGame, deletImg);
    }
}