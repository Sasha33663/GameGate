using Application.Common.Interfaces;
using Domain;
using Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Orders;
public class GetMyOrdersQueryHandler : IRequestHandler<GetMyOrdersQuery, List<Order?>>
{
    private readonly IMarketRepository _marketRepository;
    private readonly IAuthHttpClient _authHttpClient;
    public GetMyOrdersQueryHandler(IMarketRepository marketRepository, IAuthHttpClient authHttpClient)
    {
        _marketRepository = marketRepository;
        _authHttpClient = authHttpClient;
    }
    public async Task<List<Order?>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
    {
      var user= await _authHttpClient.GetSellerByCookieAsync(request.cookie);
      await _marketRepository.CreateSellerAsync(user);
      return _marketRepository.GetOrdersByName(user.UserName);
    }
}
