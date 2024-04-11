using Application.Common.Interfaces;
using Domain.Users;
using Infrastructure.Dto;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HttpClients;
public class AuthHttpClient : IAuthHttpClient
{
    private readonly HttpClient _httpClient;

    public AuthHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Buyer> GetBuyerByCookieAsync(string cookie)
    {
        var requestMessage = "https://localhost:7250/api/Auth/User/GetUser";
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(requestMessage)
        };
        if (!string.IsNullOrEmpty(cookie))
        {
            content.Headers.Add("Cookie", cookie);
        }
        var responseMessage = await _httpClient.SendAsync(content);
        responseMessage.EnsureSuccessStatusCode();
        var userDto = await responseMessage.Content.ReadFromJsonAsync<UserDto>();
        return new Buyer
        {
            UserName = userDto.UserName,
            UserId = userDto.UserId,
            Games = [],
            Money = 0,
            BoughtGames = [],
            Email = userDto?.Email,
            PhoneNumber = userDto?.PhoneNumber,
        };
    }
    public async Task<Seller> GetSellerByCookieAsync(string cookie)
    {
        var requestMessage = "https://localhost:7250/api/Auth/User/GetUser";
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(requestMessage)
        };
        if (!string.IsNullOrEmpty(cookie))
        {
            content.Headers.Add("Cookie", cookie);
        }
        var responseMessage = await _httpClient.SendAsync(content);
        responseMessage.EnsureSuccessStatusCode();
        var userDto = await responseMessage.Content.ReadFromJsonAsync<UserDto>();
        return new Seller
        {
            UserName = userDto.UserName,
            UserId = userDto.UserId,
            Games = [],
            Money = 0,
            BoughtGames = [],
            Email = userDto?.Email,
            PhoneNumber = userDto?.PhoneNumber,
        };
    }

    public async Task<Seller> GetSellerByIdAsync(string userId)
    {
        var requestMessage = "https://localhost:7250/api/Auth/User/GetUserById";
        var query = new Dictionary<string, string>
        {
            ["UserId"] = userId
        };
        var uri = requestMessage + new QueryBuilder(query);
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri)
        };
        var responseMessage = await _httpClient.SendAsync(content);
        responseMessage.EnsureSuccessStatusCode();
        var userDto = await responseMessage.Content.ReadFromJsonAsync<UserDto>();
        return new Seller
        {
            UserName = userDto.UserName,
            UserId = userDto.UserId,
            Games = [],
            Money = 0,
            BoughtGames = [],
            Email = userDto?.Email,
            PhoneNumber = userDto?.PhoneNumber,
        };
    }
}
