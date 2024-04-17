using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DeleteExpiresOrdersService;
public class DeleteExpiresOrdersService :BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public DeleteExpiresOrdersService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromMinutes(15));
            using (var scope = _serviceScopeFactory.CreateScope())
            {   var db = scope.ServiceProvider.GetService<Database>();
                var order = db.Orders.Where(x => x.DateTime < x.DateTime.Date.AddDays(2));
                db.Orders.RemoveRange(order);
                await db.SaveChangesAsync();
            }           
        }
    }
}
