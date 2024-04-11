using Domain;
using MediatR;

namespace Application.Commands.MakeOrder;
public record MakeOrderCommand(string GameName, decimal Bid, string cookie) : IRequest<Order>;