public class EffectManager
{
    
    private readonly CameraShake cameraShake;

    public EffectManager(CameraShake cameraShake)
    {
        this.cameraShake = cameraShake;
    }

    public void ShakeCamera()
    {
        cameraShake.TriggerShake();
    }
}