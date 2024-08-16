using BrightStarPhase1App.Utilities;

namespace BrightStarPhase1App.Data.Entities
{
    public class ServiceToken
    {
        public ServiceToken(string token, int expiryHours, Guid serviceId)
        {
            if (expiryHours == 0)
                expiryHours = 1;
            Token = token;
            TokenDateCreated = DateTimeProvider.Now();
            TokenExpirationDate = DateTimeProvider.Now().AddHours(expiryHours);
            ServiceId = serviceId;
        }

        public ServiceToken()
        {
            
        }

        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpirationDate { get; set; }
        public DateTime TokenDateCreated { get; set; }
        public Guid ServiceId { get; set; }

        public Service Service { get; set; }

        internal static ServiceToken CreateSession(string token, int expiryHours, Guid serviceId)
        {
            return new(token, expiryHours, serviceId);
        }

        internal bool ValidateToken()
        {
            var time = DateTimeProvider.Now();
            if (TokenExpirationDate > time)
                return true;
            return false;
        }
    }
}
