using Ugs;
using UnityEngine;

namespace State
{
    public class JumpingState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly UnityGamingServices unityGamingServices;
        private readonly Player player;
        
        
        private readonly EffectManager effectManager;
        private float jumpPower = 0f;
        private const float Deceleration = 40f;
        private const float VelocityThreshold = 0.1f;

        public JumpingState(Player player, EffectManager effectManager, StateMachine stateMachine,
            UnityGamingServices unityGamingServices)
        {
            this.player = player;
            this.effectManager = effectManager;
            this.stateMachine = stateMachine;
            this.unityGamingServices = unityGamingServices;
        }

        public void Start()
        {
            jumpPower = unityGamingServices.GetRemoteConfig().JumpPower;
        }

        public void Update()
        {
            if (Input.GetButtonUp("Jump"))
            {
                stateMachine.ChangeState(State.Falling);
            }
        }

        public void FixedUpdate()
        {
            player.MoveForward();
            
            var decreasedSpeed = Mathf.Max(0, jumpPower - (Deceleration * Time.fixedDeltaTime));
            player.GetRigidbody().velocity = new Vector3(0, decreasedSpeed, 0);
            jumpPower = decreasedSpeed;
            
            if (player.GetRigidbody().velocity.y <= VelocityThreshold)
            {
                stateMachine.ChangeState(State.Falling);
            }
        }

        public void End()
        {
            
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (CollisionUtil.IsCollisionWithWall(collision))
            {
                stateMachine.ChangeState(State.Dead);
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            
        }
    }
}