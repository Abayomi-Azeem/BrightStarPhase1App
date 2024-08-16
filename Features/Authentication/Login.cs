using BrightStarPhase1App.Common;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BrightStarPhase1App.Features.Authentication;

public record LoginCommand : IRequest<BaseResponse<LoginResponse>>
{
    public Guid ServiceId { get; set; }
    public string Password { get; set; }
}

public record LoginResponse
{
    public string Token { get; set; }
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.ServiceId).NotEmpty().NotNull();
        RuleFor(x => x.Password).NotEmpty().NotNull();
    }
}

public class LoginEndpoint(ISender mediator): ApiControllerBase
{
    private readonly ISender _meditor = mediator;

    [HttpPost]
    [Route("auth/login")]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        BaseResponse<LoginResponse> response = await _meditor.Send(request);
        return ProcessResponse(response);
    }

}




