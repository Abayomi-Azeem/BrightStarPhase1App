using System.Net;
using static BrightStarPhase1App.Common.AppEnums;

namespace BrightStarPhase1App.Common
{
    public class BaseResponse<T>
    {
        public bool HasError { get; set; }
        public string? ResponseMessage { get; set; }
        public ResponseCodes ResponseCode { get; set; }
        public T? Data { get; set; }

    }
}
