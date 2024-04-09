using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Domain.Games;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Infrastructure.Dto;
using Application.Queries.GetWithFilters;
using Application.Queries.GetWithFilters.Dto;
using Microsoft.AspNetCore.Http.Extensions;

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
            Filters =  x.Filters,
            Price = x.Price,
        });
        return result.ToList();
    }
      
}