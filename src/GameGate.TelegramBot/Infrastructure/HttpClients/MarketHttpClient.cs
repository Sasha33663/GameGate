using Domain.Games;
using Domain.Orders;
using Domain.Users;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace Infrastructure.HttpClients;

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

        var jsonResponse = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Game>>();
        var result = jsonResponse.Select(x => new Game
        {
            GameName = x.GameName,
            Description = x.Description,
            AuthorName = x.AuthorName,
            GamePreviewUrl = x.GamePreviewUrl,
            Filters = x.Filters,
            Price = x.Price,
        });

        return result.ToList();
    }
    
    public  async Task<Game> GetGameByNameAsync(string gameName)
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
    public async Task DeleteGameAsync(string gameName)
    {
        var requestMessage = "https://localhost:7037/api/games/DeleteByName";
        var query = new Dictionary<string, string>
        {
            ["GameName"] = gameName
        };
        var uri = requestMessage + new QueryBuilder(query);
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(uri)
        };
        var responseMessage = await _httpClient.SendAsync(content);
        responseMessage.EnsureSuccessStatusCode();   
    }

    public async Task <List<Game>> GetGamesByAuthor(string authorId)
    {
        var requestMessage = "https://localhost:7037/api/games/GetGameByAuthor";
        var query = new Dictionary<string, string?>
        {
            ["authorId"] = authorId
        };
        var uri = requestMessage + new QueryBuilder(query);
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri)
        };
        var responseMessage = await _httpClient.SendAsync(content);
        responseMessage.EnsureSuccessStatusCode();
        var a = await responseMessage.Content.ReadAsStringAsync();
        var jsonResponse = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Game>>();
        var result = jsonResponse.Select(x => new Game
        {
            GameName = x.GameName,
            Description = x.Description,
            AuthorId = x.AuthorId,
            GameId = x.GameId,
            GamePreviewUrl = x.GamePreviewUrl,
            AuthorName = x.AuthorName,
            Filters = x.Filters,
            Price = x.Price,
        });
        return result.ToList();
    }

    public async Task <List<Order>>  GetMyOrdersAsync(string name)
    {
        var requestMessage = "https://localhost:7061/api/market/buy/GetOrdersByName";
        var query = new Dictionary<string, string?>
        {
            ["name"] = name
        };
        var uri = requestMessage + new QueryBuilder(query);
        var content = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri)
        };

        var responseMessage = await _httpClient.SendAsync(content);
        responseMessage.EnsureSuccessStatusCode();
        var jsonResponse = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Order?>>();
        var result = jsonResponse.Select(x => new Order
        {
            GameName = x.GameName,
            Bid =x.Bid,
            BuyerId=x.BuyerId,
            BuyerName=x.BuyerName,
            Cost=x.Cost,
            GameId=x.GameId,
            OrderId=x.OrderId,
            SellerId=x.SellerId,
            SellerName=x.SellerName,
            DateTime=x.DateTime
            
        });
        return result.ToList();
    }
}
