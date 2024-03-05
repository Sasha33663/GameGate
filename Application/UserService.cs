using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService( UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task  UserRegisterAsync(string userName,string password, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = userName,

        };
      

        var result = await _userManager.CreateAsync(user, password);

        var errorMessage = "";
        if (!result.Succeeded)
        {
            // Iterate through errors and build an error message
            foreach (var error in result.Errors)
            {
                errorMessage += error.Description + " "; // Add a space between error messages
            }
            throw new Exception(errorMessage);
        }


    }
    public async Task UserLogInAsync(string userName, string password, CancellationToken cancellationToken)
    {
        
    }
}
