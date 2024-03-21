using Controls;
using Ugs;
using UI;
using UnityEngine;

namespace State
{
    public class NewGameState : IState
    {

        private readonly UnityGamingServices unityGamingServices;
        private readonly Player player;
        private readonly Ui ui;
        private readonly StateMachine stateMachine;

        public NewGameState(UnityGamingServices unityGamingServices, Player player, Ui ui, StateMachine stateMachine)
        {
            this.unityGamingServices = unityGamingServices;
            this.player = player;
            this.ui = ui;
            this.stateMachine = stateMachine;
        }

        public void Start()
        {
            player.Fuel = 30;
            player.SetPosition(0, 2);
            
            
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