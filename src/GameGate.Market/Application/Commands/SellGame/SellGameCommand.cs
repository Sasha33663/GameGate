using MediatR;

namespace Presentation.Controllers;
public record SellGameCommand (Guid orderId) : IRequest;
