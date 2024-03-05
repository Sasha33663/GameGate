using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;
public interface IUserService
{
     Task UserRegisterAsync(string userName, string password,CancellationToken cancellationToken);
    Task UserLogInAsync (string userName, string password,CancellationToken cancellationToken);
}
