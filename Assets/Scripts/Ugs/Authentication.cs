using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;

namespace Ugs
{
    public class Authentication
    {
        
        public async Task SignInAnonymously()
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        
        public string GetPlayerName()
        {
            return GetPlayerNameFromString(AuthenticationService.Instance.PlayerName);
        }

        public async void SetPlayerName(string playerName)
        {
            var name = GetPlayerName();

            if (name != playerName)
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
            }
        }
        
        public static string GetPlayerNameFromString(string playerName)
        {
            return playerName.Substring(0, playerName.IndexOf("#", StringComparison.Ordinal));
        }

    }
}