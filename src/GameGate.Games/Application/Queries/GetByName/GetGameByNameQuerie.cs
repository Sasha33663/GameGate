using Domain;
using MediatR;

namespace Application.Queries.GetByName;
public record GetGameByNameQuery(string gameName) : IRequest<Game>;