using BrightStarPhase1App.Common;
using BrightStarPhase1App.Features.Authentication;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrightStarPhase1App.Features.UnSubscribe;

public record UnSubscribeCommand : IRequest<BaseResponse<string>>
{
    public Guid ServiceId { get; set; }
    public string TokenId { get; set; }
    public string PhoneNumber { get; set; }
}
public class SubscribeCommandValidator : AbstractValidator<UnSubscribeCommand>
{
    public SubscribeCommandValidator()
    {
        RuleFor(x => x.ServiceId).NotEmpty().NotNull();
        RuleFor(x => x.TokenId).NotEmpty().NotNull();
        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().MaximumLength(20);
    }
}

public class UnSubscribeEndpoint(ISender mediator) : ApiControllerBase
{
    private readonly ISender _meditor = mediator;

    [HttpPost]
    [Route("services/unsubscribe")]
    public async Task<IActionResult> UnSubscribe(UnSubscribeCommand request)
    {
        BaseResponse<string> response = await _meditor.Send(request);
        return ProcessResponse(response);
    }

}
