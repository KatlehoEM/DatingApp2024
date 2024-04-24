using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")] //  https/localhost:5001/api
public class BaseApiController : ControllerBase
{

}