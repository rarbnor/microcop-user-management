using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authentication;

namespace Presentation.Controllers; 

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(ApiKeyAuthorizeFilter))]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> GetUser(Guid id)
    {
        var user = await _userService.GetUser(id);
        if (user is null) return NotFound($"The user with id:{id} does not exist!");
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserModel>> CreateUser([FromBody] UserCreateModel userModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Model is not valid!" });

        var user = await _userService.CreateUser(userModel);
        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult<UserModel?>> UpdateUser([FromBody] UserUpdateModel userUpdateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Model is not valid!" });

        var userModel = await _userService.UpdateUser(userUpdateModel);
        if (userModel is null) return NotFound($"The user with Id:{userUpdateModel.Id} does not exist!");
        return Ok(userModel);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserModel>> DeleteUser(Guid id)
    {
        var user = await _userService.DeleteUser(id);
        if (user is null) return NotFound($"The user with id:{id} does not exist!");
        return Ok(new { message = $"User with Id:{id} deleted successfully!" });
    }
}
