using BrightStarPhase1App.Common;
using BrightStarPhase1App.Features.Authentication;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrightStarPhase1App.Features.Subscribe;

public record SubscribeCommand: IRequest<BaseResponse<SubscribeResponse>>
{
    public Guid ServiceId { get; set; }
    public string TokenId { get; set; }
    public string PhoneNumber { get; set; }
}

public record SubscribeResponse
{
    public string SubscriptionId { get; set; }
}

public class SubscribeCommandValidator: AbstractValidator<SubscribeCommand>
{
    public SubscribeCommandValidator()
    {
        RuleFor(x => x.ServiceId).NotEmpty().NotNull();
        RuleFor(x => x.TokenId).NotEmpty().NotNull();
        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().MaximumLength(20);
    }
}

public class SubscribeEndpoint(ISender mediator) : ApiControllerBase
{
    private readonly ISender _meditor = mediator;

    [HttpPost]
    [Route("services/subscribe")]
    public async Task<IActionResult> Subscribe(SubscribeCommand request)
    {
        BaseResponse<SubscribeResponse> response = await _meditor.Send(request);
        return ProcessResponse(response);
    }

}
