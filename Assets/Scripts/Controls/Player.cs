using UnityEngine;

namespace Controls
{
    public class Player
    {
        
        private readonly GameObject gameObject;
        private readonly Rigidbody rigidbody;

        private const float ForwardSpeed = 30f;
        private const float JumpForce = 10f;
        
        private readonly float fuel;

        public Jump Jump { get; internal set; }

        public Player(GameObject gameObject, float fuel)
        {
            this.gameObject = gameObject;
            this.fuel = fuel;
            
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
            Jump = new Jump(this, JumpForce, ForwardSpeed);
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