using Unity.Services.Analytics;

namespace Ugs
{
    public class Analytics
    {
        public void SendPlayerDiedEvent(double distance)
        {
            var customEvent = new CustomEvent("PlayerDied")
            {
                { "distance", distance }
            };
            
            AnalyticsService.Instance.RecordEvent(customEvent);

        }
    }
}