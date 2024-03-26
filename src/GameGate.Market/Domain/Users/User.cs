using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Games;

namespace Domain.Users;
public abstract class User
{
    public string UserName { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
    public Game[] Games { get; set; }
}
