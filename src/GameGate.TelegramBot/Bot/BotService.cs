﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using Newtonsoft.Json;
using Telegram.Bots.Http;
using Telegram.Bots;
using Telegram.Bots.Configs;
using Telegram.Bot.Types.ReplyMarkups;
using Infrastructure.HttpClients;
using Domain.Orders;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Collections;
using System.Diagnostics.Eventing.Reader;

namespace Application;
class Program 
{
    private static readonly IMarketHttpClient _marketHttpClient;

    static Program( )
    {
        _marketHttpClient = new MarketHttpClient(new HttpClient() );
    }

    static void Main(string[] args)
    {
        var client = new TelegramBotClient("6625717406:AAEw29Fb_Brc3OuqiQuCqlbsS4TsuK_oumA");
        client.StartReceiving(Update, Error);
        Console.ReadLine();
    }
    private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
    {

        var message = update.Message;
        if (message.Text.Contains("/start"))
        {
            var replyMarkup = await TextAsync("Я продавец", "Я покупатель");
            await client.SendTextMessageAsync(message.Chat.Id, "Приветствую вас в магазине игр GameGate!\r" +
                "\n\r\nЯ - ваш личный помощник в мире игровой индустрии. Здесь вы найдете самые последние новинки, классические хиты и уникальные предложения." +
                "\r\n\r\nГотовы погрузиться в мир развлечений и приключений? Пожалуйста, укажите вашу роль, чтобы мы могли предоставить вам наилучший сервис:", replyMarkup: replyMarkup);
        }
        if (message.Text.Contains("Я продавец"))
        {
            var order =await Seller(message, client);
            await client.SendTextMessageAsync(message.Chat.Id, order);
        }
        if (message.Text.Contains("Я покупатель"))
        {
            await client.SendTextMessageAsync(message.Chat.Id, "Здесь ничего нету :)");
        }

    }
    private static async Task <string> Seller(Message message, ITelegramBotClient client)
     {
        var keyboard = new KeyboardButton[][]
        {
                     new KeyboardButton[] { "Посмотреть мои заказы","Проданные игры"}
        };
        var replyMarkup = new ReplyKeyboardMarkup(keyboard);
        await client.SendTextMessageAsync(message.Chat.Id, "Выбирите действие:", replyMarkup: replyMarkup);
       var choice = await client.GetUpdatesAsync();
        var lastMessage = choice.Last();
        var choiceText = lastMessage.Message.Text;
        if (choiceText.Contains("Посмотреть мои заказы"))
        {
            await client.SendTextMessageAsync(message.Chat.Id, "Введите имя:");
            var updatesA = await client.GetUpdatesAsync();
            var last = updatesA.Last();
            string? text = last.Message.Text;
            var orders = await _marketHttpClient?.GetMyOrdersAsync(text);
            return JsonConvert.SerializeObject(orders, Formatting.Indented);

        }
        return "";
    }
   
    private static async Task<ReplyKeyboardMarkup?> TextAsync(string message1, string message2)
    {
        var keyboard = new KeyboardButton[][]
               {
                     new KeyboardButton[] { $"{message1}",$"{message2}"}
               };
        return new ReplyKeyboardMarkup(keyboard);
    }
    private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
    }

}

