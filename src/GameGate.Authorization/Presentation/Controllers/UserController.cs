using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using Domain;
using System.Collections.Immutable;

namespace Presentation.Controllers;
[ApiController]
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
    public async Task <UserDto> FindUserAsync()
    {        
        var foundUser =await _userService.GetUserAsync(User.Identity.Name);
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
