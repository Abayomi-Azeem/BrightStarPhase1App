namespace BrightStarPhase1App.Data.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public int TokenExpiryHours { get; set; } = 1;

        public ICollection<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
        public ICollection<ServiceToken> ServiceTokens { get; set; } = new List<ServiceToken>();
    }
}
