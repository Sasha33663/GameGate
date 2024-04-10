using Domain.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users;
public sealed class Seller : User
{
   public decimal Money {  get; set; }
   public List <Game> BoughtGames {  get; set; }
   public List <Game> SoldGames { get; set; }
}
