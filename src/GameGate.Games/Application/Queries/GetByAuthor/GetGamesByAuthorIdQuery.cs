using Domain;
using MediatR;

namespace Presentation.Controllers;
public record GetGamesByAuthorIdQuery (string authorId): IRequest<List<Game>>;
