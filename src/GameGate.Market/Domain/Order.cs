using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Games;
using Domain.Users;

namespace Domain;
public sealed class Order
{
    public Guid OrderId { get; set; }
    public  Game Game { get; set; } 
    public Buyer Buyer { get; set; }
    public bool IsMade { get; set; }
    public DateTime DateTime { get; set; }
}
