using UnityEngine;

namespace State
{
    public class GroundedState : IState
    {

        private readonly Player player;
        private readonly StateMachine stateMachine;
        private readonly EffectManager effectManager;

        private Player.PhysicsState physicsState;
        public GroundedState(Player player, EffectManager effectManager, StateMachine stateMachine)
        {
            this.player = player;
            this.effectManager = effectManager;
            this.stateMachine = stateMachine;
        }

        public void Start()
        {
            physicsState = player.CreatePhysicsState();
        }

        public void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                stateMachine.ChangeState(State.Jumping);
            }
        }

        public void FixedUpdate()
        {
            physicsState.MoveForward();
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
            stateMachine.ChangeState(State.Falling);
        }
    }
}