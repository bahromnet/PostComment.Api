using Application.Api.UseCases.Users.Commands;
using Application.Api.UseCases.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.WebUI.Attributes;

namespace PostComment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiBaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand command) => Ok(await _mediatr.Send(command));

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser([FromForm] DeleteUserCommand command) => Ok(await _mediatr.Send(command));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand command) => Ok(await _mediatr.Send(command));

    [HttpGet("getall")]
    public async Task<IActionResult> GetAlUserts() => Ok(await _mediatr.Send(new GetAllUserQuery()));

    [HttpPost("getbyid")]
    public async Task<IActionResult> GetByIdUser([FromForm] Guid id) => Ok(await _mediatr.Send(new GetByIdUserQuery { Id = id }));
}
