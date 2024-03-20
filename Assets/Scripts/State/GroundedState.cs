using Controls;
using UI;
using UnityEngine;

namespace State
{
    public class GroundedState : IState
    {

        private readonly Player player;
        private readonly StateMachine stateMachine;
        private readonly EffectManager effectManager;
        private readonly Ui ui;
        
        public GroundedState(Player player, Ui ui, EffectManager effectManager, StateMachine stateMachine)
        {
            this.player = player;
            this.ui = ui;
            this.effectManager = effectManager;
            this.stateMachine = stateMachine;
        }

        public void Start()
        {
            player.ResetJumpState();
        }

        public void Update()
        {
            ui.UpdateFuel();
            if (player.Fuel >= 5f && Input.GetButtonDown("Jump"))
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