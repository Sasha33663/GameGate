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
    private readonly SignInManager <User> _signInManager;
    private readonly RoleManager <IdentityRole> _roleManager;
    public UserService( UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }
    public async Task  UserRegisterAsync(string userName,string password, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = userName,

        };
      

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Buyer");
        }

         await ErorrMessage(result);


    }
    public async Task UserLogInAsync(string userName, string password, CancellationToken cancellationToken)
    {
        var user =    await _userManager.FindByNameAsync (userName);
        if (user== null)
        {
            throw new Exception("Неверный Логин или Пароль");
        }
        var result =  await _signInManager.PasswordSignInAsync(user ,password, isPersistent: true,lockoutOnFailure: false );
        await ErorrMessage(result);

    }
    public async Task ErorrMessage(IdentityResult result)
    {
        var errorMessage = "";
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                errorMessage += error.Description + " "; 
            }
            throw new Exception(errorMessage);
        }
        

    }
    public async Task ErorrMessage(SignInResult result)
    {
        if (!result.Succeeded)
        {
            
            throw new Exception("Неверный Логин или Пароль");
        }

    }
    public async Task UserDeleteAsync(string userName, string password, CancellationToken cancellationToken)
    {
        var user   =await _userManager.FindByNameAsync(userName);
        var result =await _userManager.DeleteAsync(user);
                    await ErorrMessage(result);


    }

    public async Task GiveRoleAsync(Guid userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (roleName == null)
        {
            throw new Exception("Role can't be null");
        }
        
        if (roleName =="Admin"|| roleName == "Seller")
        {
            await _userManager.RemoveFromRoleAsync(user, "Buyer");
            await _userManager.AddToRoleAsync(user, roleName);
        }
        else
        {
            throw new Exception("Недопустимая роль");
        }
       
            
    }
}
