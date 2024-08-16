using BrightStarPhase1App.Common;
using BrightStarPhase1App.Data;
using BrightStarPhase1App.Data.Entities;
using MediatR;
using static BrightStarPhase1App.Common.AppEnums;

namespace BrightStarPhase1App.Features.Subscribe;

public class SubscribeHandler : IRequestHandler<SubscribeCommand, BaseResponse<SubscribeResponse>>
{
    private readonly AppDbContext _context;
    private readonly ILogger<SubscribeHandler> _logger;
    public SubscribeHandler(AppDbContext context, ILogger<SubscribeHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BaseResponse<SubscribeResponse>> Handle(SubscribeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[SubscribeHandler] - Request Arrived - {request.ServiceId}");
        var token = _context.ServiceTokens.FirstOrDefault(x => x.Token == request.TokenId && x.ServiceId==request.ServiceId);

        if (token == null)
            return ResponseBuilder.BuildError<SubscribeResponse>(ResponseCodes.InvalidTokenId, "Wrong TokenId");

        if(!token.ValidateToken())
            return ResponseBuilder.BuildError<SubscribeResponse>(ResponseCodes.TokenExpired, "Session Expired");

        var existingSubscriber = _context.Subscribers.FirstOrDefault(x => x.ServiceId == request.ServiceId && x.PhoneNumber == request.PhoneNumber);
        if(existingSubscriber is not null && existingSubscriber.IsActive)
            return ResponseBuilder.BuildError<SubscribeResponse>(ResponseCodes.SubscriptionExists, "User is already Subscribed");
        else if (existingSubscriber is not null && !existingSubscriber.IsActive)
        {
            existingSubscriber.Resubscribe();
            _context.Subscribers.Update(existingSubscriber);
            await _context.SaveChangesAsync();
            return ResponseBuilder.Build<SubscribeResponse>(data: new() { SubscriptionId = existingSubscriber.Id.ToString() });

        }

        var subscriber = Subscriber.CreateSubscriber(request.PhoneNumber, request.ServiceId);

        await _context.Subscribers.AddAsync(subscriber);
        var saveCount = await _context.SaveChangesAsync();

        if (saveCount > 0)
            return ResponseBuilder.Build<SubscribeResponse>(data: new() { SubscriptionId = subscriber.Id.ToString() });

        return ResponseBuilder.BuildError<SubscribeResponse>(ResponseCodes.Error, "Error Saving Subscription");

    }
}
