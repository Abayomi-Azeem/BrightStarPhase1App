using System.Net;
using static BrightStarPhase1App.Common.AppEnums;

namespace BrightStarPhase1App.Common
{
    public class ResponseBuilder
    {
        public static BaseResponse<T> Build<T>(string responseMessage = "Successful", T? data = default)
        {
            return new BaseResponse<T>
            {
                Data = data,
                ResponseCode = ResponseCodes.OK,
                ResponseMessage = responseMessage,
                HasError = false
            };
        }

        public static BaseResponse<T> BuildError<T>(ResponseCodes responseCode, string responseMessage, T? data = default)
        {
            return new BaseResponse<T>
            {
                Data = data,
                ResponseCode = responseCode,
                ResponseMessage = responseMessage,
                HasError = true,
            };
        }
    }
}
