using MediatR;

namespace Application.Commands.SellGame;
public record SellGameByOrderCommand(Guid orderId) : IRequest;
