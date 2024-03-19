using System.Threading.Tasks;
using Unity.Services.RemoteConfig;

namespace Ugs
{
    public class RemoteConfig
    {
        
        public float ForwardSpeed { get; private set;}
        
        public float JumpForce { get; private set; }
        
        public async Task RefreshConfig()
        {
            var fetchConfigsAsync = await RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes());
            
            ForwardSpeed = fetchConfigsAsync.GetFloat("ForwardSpeed", 30f);
            JumpForce = fetchConfigsAsync.GetFloat("JumpForce", 10f);
        }
    }

    public struct appAttributes
    {
    }

    public struct userAttributes
    {
    }
}