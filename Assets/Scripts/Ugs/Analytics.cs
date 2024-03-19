using Unity.Services.Analytics;

namespace Ugs
{
    public class Analytics
    {

        public void OptIn()
        {
            AnalyticsService.Instance.StartDataCollection();
        }
        
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