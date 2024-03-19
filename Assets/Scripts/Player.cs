using Ugs;
using UnityEngine;

public class Player
{
        
    private readonly GameObject gameObject;
    private readonly Rigidbody rigidbody;
    private readonly UnityGamingServices unityGamingServices;
        
    public Player(GameObject gameObject, UnityGamingServices unityGamingServices)
    {
        this.gameObject = gameObject;
        this.unityGamingServices = unityGamingServices;
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }
        
    public double GetPosition()
    {
        return gameObject.transform.position.z;
    }
        
    public Rigidbody GetRigidbody()
    {
        return rigidbody;
    }
        
    public void MoveForward()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * (unityGamingServices.GetRemoteConfig().MoveSpeed * Time.deltaTime), 0, unityGamingServices.GetRemoteConfig().ForwardSpeed * Time.deltaTime);
        gameObject.transform.position += movement;
    }

    public void Stop()
    {
        rigidbody.velocity = new Vector3(0, 0, 0);
    }

    public void SetPosition(int x, int y)
    {
        gameObject.transform.position = new Vector3(x, y, 0);
    }
}