using BrightStarPhase1App.Common;
using Microsoft.AspNetCore.Diagnostics;
using static BrightStarPhase1App.Common.AppEnums;

namespace BrightStarPhase1App.Filters
{
    public class GlobalExceptionHandler(IHostEnvironment env) : IExceptionHandler
    {
        private readonly IHostEnvironment environment = env;
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<string>()
            {
                ResponseCode = ResponseCodes.Error,
                ResponseMessage = $"Unhandled Exception Occurred! -{exception.Message}",
                HasError = true
            };
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(response);
            return true;
        }
    }
}
