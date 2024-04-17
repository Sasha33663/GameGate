using MediatR;

namespace Application.Commands.BuyGameDirectly;
public record BuyGameDirectlyCommand(string GameName, decimal Bid, string cookie) : IRequest;
