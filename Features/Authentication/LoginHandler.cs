using BrightStarPhase1App.Common;
using BrightStarPhase1App.Data;
using BrightStarPhase1App.Data.Entities;
using BrightStarPhase1App.Utilities;
using MediatR;
using Microsoft.Extensions.Options;
using static BrightStarPhase1App.Common.AppEnums;

namespace BrightStarPhase1App.Features.Authentication;

public class LoginHandler : IRequestHandler<LoginCommand, BaseResponse<LoginResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly AppConfig _appConfig;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(AppDbContext dbContext, IOptions<AppConfig> options, ILogger<LoginHandler> logger)
    {
        _dbContext = dbContext;
        _appConfig = options.Value;
        _logger = logger;
    }

    public async Task<BaseResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[LoginHandler] - Request Arrived - {request.ServiceId}");

        var service = _dbContext.Services.FirstOrDefault(x => x.Id == request.ServiceId);
        if (service is null)
            return ResponseBuilder.BuildError<LoginResponse>(ResponseCodes.InvalidServiceId, "Invalid Service Id");

        var passwordHash = SecurityUtil.PasswordHasher(request.Password, _appConfig.Salt);
        if(service.Password != passwordHash)
            return ResponseBuilder.BuildError<LoginResponse>(ResponseCodes.InvalidPassword, "Invalid Password");

        var existingToken = _dbContext.ServiceTokens.FirstOrDefault(x => x.ServiceId == request.ServiceId && x.TokenExpirationDate > DateTimeProvider.Now());
        if (existingToken is not  null)
            return ResponseBuilder.Build<LoginResponse>(data: new LoginResponse() { Token = existingToken.Token});
        var sessionToken = SecurityUtil.GenerateSessionToken();
        var serviceToken = ServiceToken.CreateSession(sessionToken, service.TokenExpiryHours, service.Id);
        _logger.LogInformation($"[LoginHandler] - Session Created - {request.ServiceId}");
        await _dbContext.ServiceTokens.AddAsync(serviceToken);
        await _dbContext.SaveChangesAsync();
        return ResponseBuilder.Build<LoginResponse>(data: new LoginResponse() { Token = sessionToken });
    }
}
