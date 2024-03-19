using Ugs;
using UnityEngine;

public class Player
{
        
    private readonly GameObject gameObject;
    private readonly Rigidbody rigidbody;
    private readonly UnityGamingServices unityGamingServices;

    private PhysicsState physicsState;
    
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

    public PhysicsState CreatePhysicsState()
    {
        var forwardSpeed = unityGamingServices.GetRemoteConfig().ForwardSpeed;
        var jumpForce = unityGamingServices.GetRemoteConfig().JumpForce;
        physicsState = new PhysicsState(this, jumpForce, forwardSpeed);
        return physicsState;
    }
    
    public PhysicsState GetCurrentPhysicsState()
    {
        return physicsState;
    }

    public void Stop()
    {
        rigidbody.velocity = new Vector3(0, 0, 0);
    }

    public void SetPosition(int x, int y)
    {
        gameObject.transform.position = new Vector3(x, y, 0);
    }

    public class PhysicsState
    {
        private float jumpTimeCounter = 0.5f; // Max jump time
        
        private const float Gravity = -100f;
        private const float MoveSpeed = 20f;
        
        private readonly float forwardSpeed;
        private readonly float jumpForce;
        
        private readonly Player player;

        public PhysicsState(Player player, float jumpForce, float forwardSpeed)
        {
            this.player = player;
            this.jumpForce = jumpForce;
            this.forwardSpeed = forwardSpeed;
        }

        public void MoveForward()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var movement = new Vector3(horizontalInput * (MoveSpeed * Time.deltaTime), 0, forwardSpeed * Time.deltaTime);
            player.gameObject.transform.position += movement;
        }
        
        /*
         * Returns true while jump is in progress
         */
        public bool JumpUpdate()
        {
            if (jumpTimeCounter <= 0)
            {
                return false;
            }
            
            player.rigidbody.velocity = Vector3.up * jumpForce;
            jumpTimeCounter -= Time.fixedDeltaTime;
            return true;
        }
        
        public void FallUpdate()
        {
            player.rigidbody.velocity += Vector3.up * (Gravity * Time.fixedDeltaTime);
        }
    }
}