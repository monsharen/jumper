using Ugs;
using UnityEngine;

namespace Controls
{
    public class Player
    {
        
        private readonly GameObject gameObject;
        private readonly Rigidbody rigidbody;
        private readonly UnityGamingServices unityGamingServices;

        public Jump Jump { get; internal set; }

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

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void ResetJumpState()
        {
            var forwardSpeed = unityGamingServices.GetRemoteConfig().ForwardSpeed;
            var jumpForce = unityGamingServices.GetRemoteConfig().JumpForce;
            Jump = new Jump(this, jumpForce, forwardSpeed);
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
}