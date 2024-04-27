using Application;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.User;

namespace Presentation.Controllers;

[Route("api/Auth/User")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task RegisterAsync([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
    {
        await _userService.UserRegisterAsync(registerDto.UserName, registerDto.Password, cancellationToken);
    }

    [HttpPost("LogIn")]
    public async Task LogInAsync([FromBody] LogInDto logInDto, CancellationToken cancellationToken)
    {
        await _userService.UserLogInAsync(logInDto.UserName, logInDto.Password, cancellationToken);
    }

    //[Authorize(Roles ="Admin")] 
    [HttpPost("Delete")]
    public async Task DeleteAsync([FromBody] DeleteDto deleteDto, CancellationToken cancellationToken)
    {
        await _userService.UserDeleteAsync(deleteDto.UserName, deleteDto.Password, cancellationToken);
    }

    //[Authorize(Roles ="Admin")]  
    [HttpGet("GetUser")]
    public async Task<UserDto> FindUserAsync()
    {
        var foundUser = await _userService.GetUserByNameAsync(User.Identity.Name);
        var user = new UserDto
        {
            UserName = foundUser.UserName,
            UserId = foundUser.Id,
            PhoneNumber = foundUser.PhoneNumber,
            Email = foundUser.Email
        };
        return user;
    }
    [HttpGet("GetUserById")]
    public async Task <UserDto> GetUserByIdAsync([FromQuery]string userId)
    {
       var foundUser = await _userService.GetUserAsync(userId);
        var user = new UserDto
        {
            UserName = foundUser.UserName,
            UserId = foundUser.Id,
            PhoneNumber = foundUser.PhoneNumber,
            Email = foundUser.Email
        };
        return user;
    }
}