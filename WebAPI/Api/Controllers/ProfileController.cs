using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Application.Interfaces.Services;

namespace WebAPI.Api.Controllers;

[ApiController]
[Route("api/profile")]
[Authorize]
public class ProfileController(IProfileService userService) : ControllerBase
{
    private readonly IProfileService _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _userService.GetUserAsync(userId);

        return result.Success ? Ok(result) : BadRequest(result);
    }

}
