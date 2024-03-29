﻿using Application.Common.Inteefaces;
using Domain;
using Microsoft.AspNetCore.Http.Extensions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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

        
        var user =  await responseMessage.Content.ReadFromJsonAsync<User> ();
        return user;
    }
}
