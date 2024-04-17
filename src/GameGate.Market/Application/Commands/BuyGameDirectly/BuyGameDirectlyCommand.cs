using MediatR;

namespace Application.Commands.SellGameDirectly;
public record BuyGameDirectlyCommand(string GameName,decimal Bid, string cookie) : IRequest;
