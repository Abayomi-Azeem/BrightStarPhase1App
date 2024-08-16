using MediatR;
using Microsoft.AspNetCore.Mvc;
using static BrightStarPhase1App.Common.AppEnums;

namespace BrightStarPhase1App.Common;

[Route("api/")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender? _sender;

    protected ISender Mediator => _sender ??= HttpContext.RequestServices.GetService<ISender>()!;


    public IActionResult ProcessResponse<T>(BaseResponse<T> response)
    {
        return response.ResponseCode switch
        {
            ResponseCodes.OK => Ok(response),
            ResponseCodes.Error => StatusCode(500, response), 
            ResponseCodes.InvalidTokenId => Unauthorized(response),
            ResponseCodes.SubscriptionNotFound => NotFound(response),
            _ => StatusCode(400, response),
        };
    }
}

