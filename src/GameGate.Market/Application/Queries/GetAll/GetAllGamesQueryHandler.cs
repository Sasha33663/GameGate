using Application.Common.Interfaces;
using Domain.Games;
using MediatR;

namespace Application.Queries.GetAll;

public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, List<Game>>
{
    private readonly IGamesHttpClient _marketHttpClient;

    public GetAllGamesQueryHandler(IGamesHttpClient marketHttpClient)
    {
        _marketHttpClient = marketHttpClient;
    }

    public async Task<List<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        return await _marketHttpClient.GetAllGamesAsync();
    }
}