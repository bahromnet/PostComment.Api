using Application.Api.UseCases.Comments.Commands;
using Application.Api.UseCases.Comments.Queries;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.WebUI.Attributes;

namespace PostComment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ApiBaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateComment([FromForm] CreateCommentCommand command) => Ok(await _mediatr.Send(command));

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteComment([FromForm] DeleteCommentCommand command) => Ok(await _mediatr.Send(command));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateComment([FromForm] UpdateCommentCommand command) => Ok(await _mediatr.Send(command));

    [HttpGet("getallcomment")]
    public async Task<IActionResult> GetAllComments() => Ok(await _mediatr.Send(new GetAllCommentQuery()));

    [HttpPost("getbyid")]
    public async Task<IActionResult> GetByIdComment([FromForm] Guid id) => Ok(await _mediatr.Send(new GetByIdCommentQuery { Id = id }));
}
