using MediatR;

namespace Presentation.Controllers;
public record DeleteByNameCommand (string gameName): IRequest;
