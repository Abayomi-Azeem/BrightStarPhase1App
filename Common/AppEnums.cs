namespace BrightStarPhase1App.Common
{
    public class AppEnums
    {
        public enum ResponseCodes
        {
            OK = 0,
            Error = 1,
            InvalidPassword=3,
            InvalidServiceId=4,
            InvalidTokenId =5,
            TokenExpired=6,
            SubscriptionExists=7,
            SubscriptionNotFound=8


        }
    }
}
