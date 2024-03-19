using System;
using System.Collections.Generic;
using System.Globalization;
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
        private StateMachine stateMachine;
        private UIDocument uiDocument;
        private VisualTreeAsset leaderboardEntry;

        public Ui(UnityGamingServices unityGamingServices, StateMachine stateMachine, UIDocument uiDocument, VisualTreeAsset leaderboardEntry)
        {
            this.unityGamingServices = unityGamingServices;
            this.stateMachine = stateMachine;
            this.uiDocument = uiDocument;
            this.leaderboardEntry = leaderboardEntry;
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

        private Label CreateLabel()
        {
            var myLabel = new Label("Hello, UIToolkit!");

            // Optionally, set some styling properties
            myLabel.style.fontSize = 14;
            myLabel.style.color = Color.white;
            return myLabel;
        }
    }
}
