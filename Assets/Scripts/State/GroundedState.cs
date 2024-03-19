using Controls;
using UnityEngine;

namespace State
{
    public class GroundedState : IState
    {

        private readonly Player player;
        private readonly StateMachine stateMachine;
        private readonly EffectManager effectManager;
        
        public GroundedState(Player player, EffectManager effectManager, StateMachine stateMachine)
        {
            this.player = player;
            this.effectManager = effectManager;
            this.stateMachine = stateMachine;
        }

        public void Start()
        {
            player.ResetJumpState();
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
            player.Jump.MoveForward();
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