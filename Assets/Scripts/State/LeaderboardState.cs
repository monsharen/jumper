using Ugs;
using UI;
using UnityEngine;

namespace State
{
    public class LeaderboardState : IState
    {
        
        private readonly Ui ui;
        private readonly UnityGamingServices unityGamingServices;

        public LeaderboardState(Ui ui, UnityGamingServices unityGamingServices)
        {
            this.ui = ui;
            this.unityGamingServices = unityGamingServices;
        }

        public void Start()
        {
            var scores = unityGamingServices.GetLeaderboards().GetScores();
            ui.ShowLeaderboard(scores);
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void End()
        {
            ui.HideLeaderboard();
        }

        public void OnCollisionEnter(Collision collision)
        {
           
        }

        public void OnCollisionExit(Collision collision)
        {
            
        }
    }
}