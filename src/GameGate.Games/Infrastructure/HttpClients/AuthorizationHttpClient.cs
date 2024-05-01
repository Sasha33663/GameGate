using Application.Common.Intefaces;
using Domain;
using System.Net.Http.Json;

namespace Infrastructure.HttpClients;

public class AuthorizationHttpClient : IAuthorizationHttpClient
{
    private readonly string _address;
    private readonly HttpClient _httpClient;
    public AuthorizationHttpClient(HttpClient httpClient, string address)
    {
        _httpClient = httpClient;
        _address = address;
    }

    public async Task<User> GetUserAsync(string cookie)
    {
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_address + "/api/Auth/User/GetUser")
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