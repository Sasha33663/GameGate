using Application.Common.Intefaces;
using Domain;
using Domain.Games;
using MediatR;

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
        if (request.PriceMaxValue < request.PriceMinValue)
        {
            throw new Exception("Price Max. Value can't be greater than Price Min. Value");
        }
        var user = await _authorizationHttpClient.GetUserAsync(request.coockie);
        (var Id, var Url) = await _imageRepository.UploadImageAsync(request.GamePreviewName, request.GamePreview);
        try
        {
            var price = new GamePrice
            {
                PriceMaxValue = request.PriceMaxValue,
                PriceMinValue = request.PriceMinValue,
                IsDirectly = request.IsDirectly,
            };
            var filters = new Filters
            {
                Genre = request.Genre,
                Kind = request.Kind,
                Creator = request.Creator
            };
            await _gameRepository.CreateAsync(new Game
            {
                GameName = request.Name,
                Description = request.Discription,
                GamePreviewUrl = Url,
                GamePreviewId = Id,
                GameId = Guid.NewGuid(),
                AuthorName = user.UserName,
                AuthorId = user.UserId,
                Filters = filters,
                Price = price
            });
        }
        catch (Exception ex)
        {
            await _imageRepository.DeleteImageAsync(Id);
            throw ex;
        }
    }
}