using BrightStarPhase1App.Utilities;

namespace BrightStarPhase1App.Data.Entities
{
    public class Subscriber
    {
        public Subscriber(string phoneNumber, Guid serviceId)
        {
            Id = Guid.NewGuid();
            PhoneNumber = phoneNumber;
            ServiceId = serviceId;
            DateCreated = DateTimeProvider.Now();
            IsActive = true;
        }

        public Guid Id { get; set; }        
        public string PhoneNumber { get; set; } 
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public Guid ServiceId { get; set; }          

        public Service Service { get; set; }

        public static Subscriber CreateSubscriber(string phoneNumber, Guid serviceId)
        {
            return new(phoneNumber, serviceId);
        }

        internal void Resubscribe()
        {
            IsActive = true;
            DateCreated = DateTimeProvider.Now();
            DateModified = null;
        }

        internal void UnSubscribe()
        {
            IsActive = false;
            DateModified = DateTimeProvider.Now();
        }
    }
}
