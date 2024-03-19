using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Leaderboards;
using UnityEngine;

namespace Ugs
{
    public class Leaderboards
    {
        private const string LeaderboardId = "LongestRun";
        
        private readonly List<LeaderboardScore> leaderboardScores = new List<LeaderboardScore>();
        
        public List<LeaderboardScore> GetScores()
        {
            return leaderboardScores;
        }
        
        public async Task RefreshScores()
        {
            var leaderboardScoresPage = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
            
            leaderboardScores.Clear();
            foreach (var score in leaderboardScoresPage.Results)
            {
                leaderboardScores.Add(new LeaderboardScore
                {
                    PlayerName = Authentication.GetPlayerNameFromString(score.PlayerName),
                    Score = score.Score
                });
            }
        }
        
        public async void SubmitScore(double score)
        {
            var submitScoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
            Debug.Log(JsonConvert.SerializeObject(submitScoreResponse));
        }
    }

    public class LeaderboardScore 
    {
        public string PlayerName;
        public double Score;
    }
     
}