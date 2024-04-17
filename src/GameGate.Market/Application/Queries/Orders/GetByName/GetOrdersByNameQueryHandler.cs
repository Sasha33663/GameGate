﻿using Application.Common.Interfaces;
using Domain.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Orders.GetByName;
public class GetOrdersByNameQueryHandler :IRequestHandler<GetOrdersByNameQuery, List<Order?>>
{
    private readonly IMarketRepository _marketRepository;

    public async Task<List<Order?>> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        return _marketRepository.GetOrdersByName(request.Name);
    }
}
