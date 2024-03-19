using Ugs;
using UnityEngine;

namespace State
{
    public class FallingState : IState
    {
        
        private readonly StateMachine stateMachine;
        private readonly UnityGamingServices unityGamingServices;
        private readonly EffectManager effectManager;
        private readonly Player player;

        private Player.PhysicsState physicsState;

        public FallingState(Player player, UnityGamingServices unityGamingServices, EffectManager effectManager, StateMachine stateMachine)
        {
            this.unityGamingServices = unityGamingServices;
            this.player = player;
            this.effectManager = effectManager;
            this.stateMachine = stateMachine;
        }

        public void Start()
        {
            physicsState = player.GetCurrentPhysicsState();
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            physicsState.MoveForward();
            physicsState.FallUpdate();
            
            if (player.GetRigidbody().transform.position.y < -1)
            {
                stateMachine.ChangeState(State.Dead);
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
                return;
            }
            
            if (collision.gameObject.CompareTag("Steel"))
            {
                stateMachine.ChangeState(State.Grounded);
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            
        }
    }
}