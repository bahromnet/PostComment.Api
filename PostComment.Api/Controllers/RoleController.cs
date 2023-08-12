using Application.Api.UseCases.Permissions.Commands;
using Application.Api.UseCases.Permissions.Queries;
using Application.Api.UseCases.Roles.Commands;
using Application.Api.UseCases.Roles.Queries;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.WebUI.Attributes;

namespace PostComment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ApiBaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromForm] CreateRoleCommand command) => Ok(await _mediatr.Send(command));

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteRole([FromForm] DeleteRoleCommand command) => Ok(await _mediatr.Send(command));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleCommand command) => Ok(await _mediatr.Send(command));

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllRoles() => Ok(await _mediatr.Send(new GetAllRoleQuery()));

    [HttpPost("getbyid")]
    public async Task<IActionResult> GetByIdRole([FromForm] Guid id) => Ok(await _mediatr.Send(new GetByIdRoleQuery { Id = id }));
}
