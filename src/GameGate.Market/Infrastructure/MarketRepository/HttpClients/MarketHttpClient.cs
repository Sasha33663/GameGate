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

namespace Infrastructure.MarketRepository.HttpClients;
public class MarketHttpClient : IMarketHttpClient
{
    private readonly HttpClient _httpClient;
    public MarketHttpClient(HttpClient httpClient)
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

        var jsonResponse = await responseMessage.Content.ReadFromJsonAsync <IEnumerable<Game>>();
        var result = jsonResponse.Select(x => new Game
        {
            GameName = x.GameName,
            Description = x.Description,
        });

        return result.ToList();
    }
}