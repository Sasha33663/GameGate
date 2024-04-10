using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Games;

namespace Domain.Users;
public sealed class Buyer : User
{
    public List <string?> BoughtGames { get; set; }
    public decimal Money { get; set; }
}
