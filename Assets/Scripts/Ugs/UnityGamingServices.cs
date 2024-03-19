using System.Threading.Tasks;
using Unity.Services.Core;

namespace Ugs
{
    public class UnityGamingServices
    {

        private readonly Leaderboards leaderboards;
        private readonly Authentication authentication;
        private readonly Analytics analytics;
        private readonly RemoteConfig remoteConfig;
        
        public UnityGamingServices(Leaderboards leaderboards, Authentication authentication, Analytics analytics, RemoteConfig remoteConfig)
        {
            this.leaderboards = leaderboards;
            this.authentication = authentication;
            this.analytics = analytics;
            this.remoteConfig = remoteConfig;
        }
        public async Task Init()
        {
            await UnityServices.InitializeAsync();
            await authentication.SignInAnonymously();
            await remoteConfig.RefreshConfig();
        }
        
        public RemoteConfig GetRemoteConfig()
        {
            return remoteConfig;
        }
        
        public Analytics GetAnalytics()
        {
            return analytics;
        }
        
        public Leaderboards GetLeaderboards()
        {
            return leaderboards;
        }
        
        public Authentication GetAuthentication()
        {
            return authentication;
        }
    }
}