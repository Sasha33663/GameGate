using Application;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers;
[Route("api/Auth/Role")]
public class RoleController : Controller
{
    private readonly IUserService _userService;
    public RoleController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("CreateRole")]
    //[Authorize(Roles = "Admin")]

    public async Task CreateRole([FromBody] RoleDto roleDto)
    {
        await _userService.MakeRoleAsync(roleDto.roleName);

    }
    [HttpPost("GiveRole")]
    //[Authorize(Roles = "Admin")]
    public async Task GiveRole([FromBody] GiveRoleDto giveRoleDto)
    {
        await _userService.GiveRoleAsync(giveRoleDto.userId, giveRoleDto.roleName);
    }
}
