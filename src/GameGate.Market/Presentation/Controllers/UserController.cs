using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers;
[ApiController]
[Route("api/market/user")]
public class UserController :Controller
{
    private readonly ISender _sender;
    public UserController (ISender sender)
    {
        _sender = sender;
    }
    //[HttpGet("GetAllUsers")]
    //public async Task<List<UserDto>> GetAllUserAsync()
    //{
    //}
}
