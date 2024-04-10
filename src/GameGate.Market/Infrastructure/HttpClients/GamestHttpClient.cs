using Application.Common.Interfaces;
using Application.Queries.GetWithFilters.Dto;
using Domain.Games;
using Domain.Users;
using Infrastructure.Dto;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http.Json;

namespace Infrastructure.HttpClients;

public class GamestHttpClient : IGamesHttpClient
{
    private readonly HttpClient _httpClient;

    public GamestHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Game>> GetAllGamesAsync()
    {
        var requestMessage = "https://localhost:7037/api/games/GetAllGames";
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(requestMessage)
        };

        var responseMessage = await _httpClient.SendAsync(content);
        responseMessage.EnsureSuccessStatusCode();

        var jsonResponse = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Game>>();
        var result = jsonResponse.Select(x => new Game
        {
            GameName = x.GameName,
            Description = x.Description,
            Author = x.Author,
            GamePreviewUrl = x.GamePreviewUrl,
            Filters = x.Filters,
            Price = x.Price,
        });

        return result.ToList();
    }

    public async Task<List<Game>> GetGamesWithFiltersAsync(FilteredGameDto filteredGame)
    {
        var requestMessage = "https://localhost:7037/api/games/GetGameWithFilter";
        var query = new Dictionary<string, string?>
        {
            ["GameName"] = filteredGame?.GameName ?? string.Empty,
            ["Creator"] = filteredGame?.Creator ?? string.Empty,
            ["Genre"] = filteredGame?.Genre ?? string.Empty,
            ["Kind"] = filteredGame?.Kind ?? string.Empty,
            ["PriceMaxValue"] = filteredGame?.PriceMaxValue?.ToString() ?? string.Empty,
            ["PriceMinValue"] = filteredGame?.PriceMinValue?.ToString() ?? string.Empty,
            ["IsDirectly"] = filteredGame?.IsDirectly?.ToString() ?? string.Empty,
        };
        var uri = requestMessage + new QueryBuilder(query);
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri)
        };
        var responseMessage = await _httpClient.SendAsync(content);
        responseMessage.EnsureSuccessStatusCode();
        var jsonResponse = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Game>>();
        var result = jsonResponse.Select(x => new Game
        {
            GameName = x.GameName,
            Description = x.Description,
            Author = x.Author,
            Filters = x.Filters,
            Price = x.Price,
        });
        return result.ToList();
    }

    public async Task<Game> GetGameByNameAsync(string gameName)
    {
        var requestMessage = "https://localhost:7037/api/games/GetGameByName";
        var query = new Dictionary<string, string>
        {
            ["GameName"] = gameName
        };
        var uri = requestMessage + new QueryBuilder(query);
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri)
        };
        var responseMessage = await _httpClient.SendAsync(content);
        responseMessage.EnsureSuccessStatusCode();
        var jsonResponse = await responseMessage.Content.ReadFromJsonAsync<Game>();
        return jsonResponse;
    }

    public async Task<Buyer> GetUserAsync(string cookie)
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
}