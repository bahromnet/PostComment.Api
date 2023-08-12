using Application.Api.UseCases.Permissions.Commands;
using Application.Api.UseCases.Permissions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace PostComment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionController : ApiBaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> CreatePermission([FromForm] CreatePermissionCommand command) => Ok(await _mediatr.Send(command));

    [HttpDelete("delete")]
    public async Task<IActionResult> DeletePermission([FromForm] DeletePermissionCommand command) => Ok(await _mediatr.Send(command));

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePermission([FromForm] UpdatePermissionCommand command) => Ok(await _mediatr.Send(command));

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllPermissions() => Ok(await _mediatr.Send(new GetAllPermissionQuery()));

    [HttpPost("getbyid")]
    public async Task<IActionResult> GetByIdPermission([FromForm] Guid id) => Ok(await _mediatr.Send(new GetByIdPermissionQuery { Id = id }));
}
