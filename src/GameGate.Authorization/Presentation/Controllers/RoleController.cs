using Application;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;

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
    public async Task CreateRole([FromBody] RoleDto roleDto)
    {
        await _userService.MakeRoleAsync(roleDto.roleName);
    }

    [HttpPost("GiveRole")]
    public async Task GiveRole([FromBody] GiveRoleDto giveRoleDto)
    {
        await _userService.GiveRoleAsync(giveRoleDto.userId, giveRoleDto.roleName);
    }
}