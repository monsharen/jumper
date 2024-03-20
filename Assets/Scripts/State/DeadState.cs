using System;
using Controls;
using Ugs;
using UI;
using UnityEngine;

namespace State
{
    public class DeadState : IState
    {

        private readonly Player player;
        private readonly Ui ui;
        private readonly EffectManager effectManager;
        private readonly UnityGamingServices unityGamingServices;
        private readonly StateMachine stateMachine;
        public DeadState(Player player, Ui ui, EffectManager effectManager, UnityGamingServices unityGamingServices, StateMachine stateMachine)
        {
            this.player = player;
            this.ui = ui;
            this.effectManager = effectManager;
            this.unityGamingServices = unityGamingServices;
            this.stateMachine = stateMachine;
        }
        public void Start()
        {
            effectManager.ShakeCamera();
            var distance = Math.Round(player.GetPosition());
            Debug.Log("Player Died at " + distance);
            unityGamingServices.GetLeaderboards().SubmitScore(distance);
            
            var task = unityGamingServices.GetLeaderboards().RefreshScores();
            task.ContinueWith(t =>
            {
                stateMachine.ChangeState(State.Leaderboard);
            });
            
            player.Stop();
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