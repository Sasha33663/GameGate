using Application.Common.Inteefaces;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GameRepository.HttpClients;
public class AuthorizationHttpClient : IAuthorizationHttpClient
{
    private readonly HttpClient _httpClient;
    public AuthorizationHttpClient (HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task GetUserAsync( )
    {
        
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"http://localhost:5134/api/Auth/User/GetUser?={}")

        };
        var responseMessage = await _httpClient.SendAsync(content);
    }
}
