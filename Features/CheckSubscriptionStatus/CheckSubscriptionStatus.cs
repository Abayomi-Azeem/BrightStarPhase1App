using BrightStarPhase1App.Common;
using BrightStarPhase1App.Features.Subscribe;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrightStarPhase1App.Features.CheckSubscriptionStatus;

public record CheckSubscriptionStatusQuery: IRequest<BaseResponse<CheckSubscriptionStatusResponse>>
{
    public Guid ServiceId { get; set; }
    public string TokenId { get; set; }
    public string PhoneNumber { get; set; }
}

public record CheckSubscriptionStatusResponse
{
    public bool IsSubscribed { get; set; }
    public DateTime SubscriptionDate { get; set; }
    public DateTime? UnSubscriptionDate { get; set; }
}

public class CheckSubscriptionStatusQueryValidator : AbstractValidator<CheckSubscriptionStatusQuery>
{
    public CheckSubscriptionStatusQueryValidator()
    {
        RuleFor(x => x.ServiceId).NotEmpty().NotNull();
        RuleFor(x => x.TokenId).NotEmpty().NotNull();
        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().MaximumLength(20);
    }
}

public class CheckSubscriptionStatusEndpoint(ISender mediator) : ApiControllerBase
{
    private readonly ISender _meditor = mediator;

    [HttpPost]
    [Route("services/status")]
    public async Task<IActionResult> CheckSubscriptionStatus(CheckSubscriptionStatusQuery request)
    {
        BaseResponse<CheckSubscriptionStatusResponse> response = await _meditor.Send(request);
        return ProcessResponse(response);
    }

}