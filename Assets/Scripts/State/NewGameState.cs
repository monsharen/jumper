using Controls;
using UnityEngine;

namespace State
{
    public class NewGameState : IState
    {

        private readonly Player player;
        private readonly StateMachine stateMachine;

        public NewGameState(Player player, StateMachine stateMachine)
        {
            this.player = player;
            this.stateMachine = stateMachine;
        }

        public void Start()
        {
            player.Stop();
            player.SetPosition(0, 2);
            
            stateMachine.ChangeState(State.Grounded);
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void End()
        {
            
        }

        public void OnCollisionEnter(Collision collision)
        {
            
        }

        public void OnCollisionExit(Collision collision)
        {
            
        }
    }
}