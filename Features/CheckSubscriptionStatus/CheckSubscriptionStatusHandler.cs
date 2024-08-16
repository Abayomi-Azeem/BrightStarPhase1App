using BrightStarPhase1App.Common;
using BrightStarPhase1App.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static BrightStarPhase1App.Common.AppEnums;

namespace BrightStarPhase1App.Features.CheckSubscriptionStatus
{
    public class CheckSubscriptionStatusHandler : IRequestHandler<CheckSubscriptionStatusQuery, BaseResponse<CheckSubscriptionStatusResponse>>
    {
        private readonly ILogger<CheckSubscriptionStatusHandler> _logger;
        private readonly AppDbContext _context;

        public CheckSubscriptionStatusHandler(ILogger<CheckSubscriptionStatusHandler> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<BaseResponse<CheckSubscriptionStatusResponse>> Handle(CheckSubscriptionStatusQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"[UnSubscribeHandler] - Request Arrived - {request.ServiceId}");
            var token = _context.ServiceTokens.FirstOrDefault(x => x.Token == request.TokenId && x.ServiceId == request.ServiceId);

            if (token == null)
                return ResponseBuilder.BuildError<CheckSubscriptionStatusResponse>(ResponseCodes.InvalidTokenId, "Wrong TokenId");

            if (!token.ValidateToken())
                return ResponseBuilder.BuildError<CheckSubscriptionStatusResponse>(ResponseCodes.TokenExpired, "Session Expired");

            var subscriber = _context.Subscribers.FirstOrDefault(x => x.ServiceId == request.ServiceId && x.PhoneNumber == request.PhoneNumber);
            if (subscriber is null)
                return ResponseBuilder.BuildError<CheckSubscriptionStatusResponse>(ResponseCodes.SubscriptionNotFound, "Subsciption Not Found");
            return ResponseBuilder.Build<CheckSubscriptionStatusResponse>(data: new() { IsSubscribed = subscriber.IsActive, SubscriptionDate = subscriber.DateCreated, UnSubscriptionDate = subscriber.DateModified });

        }
    }
}
