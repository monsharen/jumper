using UnityEngine;

namespace Controls
{
    public class Jump
    {
        private float jumpTimeCounter = 0.5f; // Max jump time
        
        private const float Gravity = -100f;
        private const float MoveSpeed = 20f;
        
        private readonly float forwardSpeed;
        private readonly float jumpForce;
        
        private readonly Player player;

        public Jump(Player player, float jumpForce, float forwardSpeed)
        {
            this.player = player;
            this.jumpForce = jumpForce;
            this.forwardSpeed = forwardSpeed;
        }

        public void MoveForward()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var movement = new Vector3(horizontalInput * (MoveSpeed * Time.deltaTime), 0, forwardSpeed * Time.deltaTime);
            player.GetGameObject().transform.position += movement;
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
            
            player.GetRigidbody().velocity = Vector3.up * jumpForce;
            jumpTimeCounter -= Time.fixedDeltaTime;
            return true;
        }
        
        public void FallUpdate()
        {
            player.GetRigidbody().velocity += Vector3.up * (Gravity * Time.fixedDeltaTime);
        }
    }
}