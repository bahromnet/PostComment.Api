using Application.Api.UseCases.Posts.Commands;
using Application.Api.UseCases.Posts.Queries;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.WebUI.Attributes;

namespace PostComment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ApiBaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostCommand command) => Ok(await _mediatr.Send(command));

    [HttpDelete("delete")]
    public async Task<IActionResult> DeletePost([FromForm] DeletePostCommand command) => Ok(await _mediatr.Send(command));

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePost([FromForm] UpdatePostCommand command) => Ok(await _mediatr.Send(command));

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllPosts() => Ok(await _mediatr.Send(new GetAllPostQuery()));

    [HttpPost("getbyid")]
    public async Task<IActionResult> GetByIdPost([FromForm] Guid id) => Ok(await _mediatr.Send(new GetByIdPostQuery { PostId = id }));
}
