using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PostComment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiBaseController : Controller
{
    protected IMediator _mediatr => HttpContext.RequestServices.GetRequiredService<IMediator>();
}
