using System.Threading.Tasks;
using Unity.Services.RemoteConfig;

namespace Ugs
{
    public class RemoteConfig
    {
        
        public float MoveSpeed { get; private set;}
        public float ForwardSpeed { get; private set;}
        public float JumpPower { get; private set; }
        public float Gravity { get; private set; }
        
        public float MaxFallSpeed { get; private set; }

        public async Task RefreshConfig()
        {
            var fetchConfigsAsync = 
                await RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes());
            
            JumpPower = fetchConfigsAsync.GetFloat("jumpPower", 5f);
            Gravity = fetchConfigsAsync.GetFloat("gravity", 20f);
            MaxFallSpeed = fetchConfigsAsync.GetFloat("maxFallSpeed", 20f);
            MoveSpeed = fetchConfigsAsync.GetFloat("moveSpeed", 10f);
            ForwardSpeed = fetchConfigsAsync.GetFloat("forwardSpeed", 30f);
        }
    }

    struct userAttributes {
    }

    struct appAttributes {
    }
}