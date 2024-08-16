using BrightStarPhase1App.Common;
using BrightStarPhase1App.Data.Entities;
using BrightStarPhase1App.Data;
using BrightStarPhase1App.Features.Subscribe;
using MediatR;
using static BrightStarPhase1App.Common.AppEnums;

namespace BrightStarPhase1App.Features.UnSubscribe;

public class UnSubscribeHandler : IRequestHandler<UnSubscribeCommand, BaseResponse<string>>
{
    private readonly AppDbContext _context;
    private readonly ILogger<UnSubscribeHandler> _logger;
    public UnSubscribeHandler(AppDbContext context, ILogger<UnSubscribeHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BaseResponse<string>> Handle(UnSubscribeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[UnSubscribeHandler] - Request Arrived - {request.ServiceId}");
        var token = _context.ServiceTokens.FirstOrDefault(x => x.Token == request.TokenId && x.ServiceId == request.ServiceId);

        if (token == null)
            return ResponseBuilder.BuildError<string>(ResponseCodes.InvalidTokenId, "Wrong TokenId");

        if (!token.ValidateToken())
            return ResponseBuilder.BuildError<string>(ResponseCodes.TokenExpired, "Session Expired");

        var existingSubscriber = _context.Subscribers.FirstOrDefault(x => x.ServiceId == request.ServiceId && x.PhoneNumber == request.PhoneNumber);
        if (existingSubscriber is null)
            return ResponseBuilder.BuildError<string>(ResponseCodes.SubscriptionNotFound, "User is Not Subscribed");
        else if(existingSubscriber.IsActive==false)
            return ResponseBuilder.BuildError<string>(ResponseCodes.SubscriptionNotFound, "User has already UnSubscribed");

        existingSubscriber.UnSubscribe();

        _context.Subscribers.Update(existingSubscriber);
        var saveCount = await _context.SaveChangesAsync();

        if (saveCount > 0)
            return ResponseBuilder.Build<string>();

        return ResponseBuilder.BuildError<string>(ResponseCodes.Error, "Error Updating Subscription");

    }
}
