using Domain;
using MediatR;

namespace Presentation.Controllers;
public record BuyGameCommand(string GameName, decimal Bid, string cookie) : IRequest<Order>;