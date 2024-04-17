using Domain.Games;
using MediatR;

namespace Application.Queries.Library;
public record GetMyGamesQuery(string Cookie) : IRequest<List<Game?>>;
