using System;
using System.Collections.Generic;
using System.Globalization;
using Controls;
using State;
using Ugs;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class Ui
    {
        private static readonly string PlayerNameTextFieldName = "PlayerName";
        private UnityGamingServices unityGamingServices;
        private Player player;
        private StateMachine stateMachine;
        private UIDocument uiDocument;
        private UIDocument hudDocument;
        private VisualTreeAsset leaderboardEntry;
        private ProgressBar progressBar;

        public Ui(UnityGamingServices unityGamingServices, Player player, StateMachine stateMachine, UIDocument uiDocument, UIDocument hudDocument, VisualTreeAsset leaderboardEntry)
        {
            this.unityGamingServices = unityGamingServices;
            this.player = player;
            this.stateMachine = stateMachine;
            this.uiDocument = uiDocument;
            this.hudDocument = hudDocument;
            this.leaderboardEntry = leaderboardEntry;
            progressBar = hudDocument.rootVisualElement.Q<ProgressBar>("FuelProgressBar");
        }

        public void ShowLeaderboard(List<LeaderboardScore> scores)
        {
            uiDocument.enabled = true;
            var playerName = unityGamingServices.GetAuthentication().GetPlayerName();
            uiDocument.rootVisualElement.Q<TextField>(PlayerNameTextFieldName).value = playerName;

            var visualElement = uiDocument.rootVisualElement.Q<VisualElement>("LeaderboardEntries");
            visualElement.Clear();
            for (int i = 0; i < scores.Count; i++)
            {
                var score = scores[i].Score;
                var name = scores[i].PlayerName;
                AddLeaderboardEntry(visualElement, i+1, name, score);
            }
        
            uiDocument.rootVisualElement.Q<Button>("PlayButton").clicked += () =>
            {
                var playerNameToSave = uiDocument.rootVisualElement.Q<TextField>(PlayerNameTextFieldName).value;
                unityGamingServices.GetAuthentication().SetPlayerName(playerNameToSave);
                stateMachine.ChangeState(State.State.NewGame);
            };
        }
    
        private void AddLeaderboardEntry(VisualElement visualElement, int position, string playerName, double score)
        {
            try
            {
                var entryElement = leaderboardEntry.Instantiate();
                entryElement.Q<Label>("Position").text = position.ToString(CultureInfo.InvariantCulture) + ". ";
                entryElement.Q<Label>("PlayerName").text = playerName;
                entryElement.Q<Label>("Score").text = score.ToString(CultureInfo.InvariantCulture);
                visualElement.Add(entryElement);
            } catch (Exception e)
            {
                Debug.Log("Failed to add leaderboard entry: " + e.Message);
            }
        }
    
        public void HideLeaderboard()
        {
            uiDocument.enabled = false;
        }

        public void UpdateFuel()
        {
            if (Math.Abs(progressBar.value - player.Fuel) > 0.5f)
            {
                progressBar.value = player.Fuel;                
            }
        }

        public void ShowHud()
        {
            hudDocument.enabled = true;
        }

        public void HideHud()
        {
            hudDocument.enabled = false;
        }
    }
}
