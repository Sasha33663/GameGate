using MediatR;

namespace Application.Commands.SellGameByOrder;
public record SellGameByOrderCommand(Guid orderId) : IRequest;
