namespace BrightStarPhase1App.Utilities
{
    public class DateTimeProvider
    {
        public static DateTime Now() => DateTime.UtcNow.AddHours(1);
    }
}
