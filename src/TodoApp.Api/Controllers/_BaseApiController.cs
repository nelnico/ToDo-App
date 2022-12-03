using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class BaseApiController : ControllerBase
{
}