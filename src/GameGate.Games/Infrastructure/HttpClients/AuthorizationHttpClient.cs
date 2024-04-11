using Application.Common.Intefaces;
using Domain;
using System.Net.Http.Json;

namespace Infrastructure.HttpClients;

public class AuthorizationHttpClient : IAuthorizationHttpClient
{
    private readonly HttpClient _httpClient;

    public AuthorizationHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User> GetUserAsync(string cookie)
    {
        var requestMessage = "http://localhost:5134/api/Auth/User/GetUser";
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
        var user = await responseMessage.Content.ReadFromJsonAsync<User>();
        return user;
    }
}