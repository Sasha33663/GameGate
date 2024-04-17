﻿using Domain.Orders;
using MediatR;

namespace Application.Queries.Orders;
public sealed record GetMyOrdersQuery(string cookie) : IRequest<List<Order?>>;
