using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task UserRegisterAsync(string userName, string password, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = userName,
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            await ErorrMessage(result);
        }
        await _userManager.AddToRoleAsync(user, "Buyer");
    }

    public async Task UserLogInAsync(string userName, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            throw new Exception("Неверный Логин или Пароль");
        }
        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
        await ErorrMessage(result);
    }

    public async Task ErorrMessage(IdentityResult result)
    {
        var errorMessage = "";

        foreach (var error in result.Errors)
        {
            errorMessage += error.Description + " ";
        }
        throw new Exception(errorMessage);
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
        var user = await _userManager.FindByNameAsync(userName);
        var result = await _userManager.DeleteAsync(user);
        await ErorrMessage(result);
    }

    public async Task MakeRoleAsync(string roleName)

    {
        if (roleName != "Admin" && roleName != "Seller" && roleName != "Buyer")
        {
            throw new Exception("Недопустимая роль");
        }

        var role = new IdentityRole(roleName);
        await _roleManager.CreateAsync(role);
    }

    public async Task GiveRoleAsync(Guid userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new Exception("Пользователь не найден");
        }
        if (roleName != "Admin" && roleName != "Seller" && roleName != "Buyer")
        {
            throw new Exception("Недопустимая роль");
        }
        var result = await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<User> GetUserByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<User> GetUserAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }
}