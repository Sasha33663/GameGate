using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Inteefaces;
public interface IAuthorizationHttpClient
{
    Task GetUserAsync();

}
