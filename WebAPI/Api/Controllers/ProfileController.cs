using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Interfaces.Security;
using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Api.Controllers;

[ApiController]
[Route("api/profile")]
[Authorize(Policy = "VerifiedUser")]
public class ProfileController(
    IApplicationUserService userService,
    IAuthenticatedUser user
) : ControllerBase
{
    private readonly IApplicationUserService _userService = userService;
    private readonly IAuthenticatedUser _user = user;

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var result = await _userService.GetUserAsync(_user.Id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
