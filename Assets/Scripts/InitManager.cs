using Ugs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        var unityGamingServices = new UnityGamingServices(
            new Leaderboards(), new Authentication(), new Analytics(), new RemoteConfig());
        var init = unityGamingServices.Init();
        init.ContinueWith(task =>
        {
            SceneManager.LoadScene("Scenes/PlayScene");
        });
    }
}
