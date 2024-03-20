using Controls;
using UI;
using UnityEngine;

namespace State
{
    public class NewGameState : IState
    {

        private readonly Player player;
        private readonly Game game;
        private readonly Ui ui;
        private readonly StateMachine stateMachine;

        public NewGameState(Game game, Player player, Ui ui, StateMachine stateMachine)
        {
            this.game = game;
            this.player = player;
            this.ui = ui;
            this.stateMachine = stateMachine;
        }

        public void Start()
        {
            game.StartNewGame();
            
            ui.UpdateFuel();
            ui.ShowHud();
            
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