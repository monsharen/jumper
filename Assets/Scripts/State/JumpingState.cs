using Controls;
using Ugs;
using UnityEngine;

namespace State
{
    public class JumpingState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly Player player;
        private readonly EffectManager effectManager;
        

        public JumpingState(Player player, EffectManager effectManager, StateMachine stateMachine)
        {
            this.player = player;
            this.effectManager = effectManager;
            this.stateMachine = stateMachine;
        }

        public void Start()
        {

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
            player.Jump.MoveForward();

            if (!player.Jump.JumpUpdate())
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