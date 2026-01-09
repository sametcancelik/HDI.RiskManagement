using HDI.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace HDI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    [NonAction]
    public ObjectResult ActionResultInstance<T>(ApiResponse<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }

    [NonAction]
    public ObjectResult ActionResultInstance(ApiResponse response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}
