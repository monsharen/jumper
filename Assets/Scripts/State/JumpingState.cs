using Ugs;
using UnityEngine;

namespace State
{
    public class JumpingState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly UnityGamingServices unityGamingServices;
        private readonly Player player;
        private Player.PhysicsState physicsState;
        
        private readonly EffectManager effectManager;
        
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
            physicsState = player.GetCurrentPhysicsState();
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
            physicsState.MoveForward();

            if (!physicsState.JumpUpdate())
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