using Domain.Orders;
using MediatR;

namespace Application.Queries.Orders.GetByName;
public record GetOrdersByNameQuery(string Name) : IRequest<List<Order?>>;
