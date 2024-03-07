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

namespace Presentation.Controllers;
[ApiController]
[Route("api/Auth/User")]
public class UserController : Controller
{

    private readonly IUserService _userService;
    public UserController (IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("Register")]
    public async Task  RegisterAsync([FromBody]RegisterDto registerDto,CancellationToken cancellationToken)
    {
       await _userService.UserRegisterAsync(registerDto.UserName, registerDto.Password, cancellationToken);
    }
    [HttpPost("LogIn")]
    public async Task LogInAsync([FromBody]LogInDto logInDto,CancellationToken cancellationToken)
    {
        await _userService.UserLogInAsync (logInDto.UserName, logInDto.Password, cancellationToken);
    }
    [HttpPost("Delete")]
    public async Task DeleteAsync([FromBody]DeleteDto deleteDto,CancellationToken cancellationToken)
    {
        await _userService.UserDeleteAsync(deleteDto.UserName, deleteDto.Password, cancellationToken);
    }
   
    
}
