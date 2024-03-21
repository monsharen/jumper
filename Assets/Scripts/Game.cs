using Controls;
using State;
using Ugs;
using UI;
using UnityEngine;
using UnityEngine.UIElements;
using static State.State;

public class Game : MonoBehaviour
{
    public UIDocument uiDocument;
    public UIDocument hudUiDocument;
    public VisualTreeAsset leaderboardEntry;
    public GameObject playerObject;
    
    private StateMachine stateMachine;
    async void Start()
    {
        stateMachine = new StateMachine();
        var unityGamingServices = new UnityGamingServices(new Leaderboards(), new Authentication(), new Analytics(), new RemoteConfig());
        await unityGamingServices.Init();
        
        var player = CreatePlayer();
        
        var ui = new Ui(unityGamingServices, player, stateMachine, uiDocument, hudUiDocument, leaderboardEntry);
        var effectManager = GetEffectManager();
        stateMachine.Register(Dead, new DeadState(player, ui, effectManager, unityGamingServices, stateMachine));
        stateMachine.Register(Falling, new FallingState(player, unityGamingServices, effectManager, stateMachine));
        stateMachine.Register(Jumping, new JumpingState(player, ui, effectManager, stateMachine));
        stateMachine.Register(Grounded, new GroundedState(player, ui, effectManager, stateMachine));
        stateMachine.Register(Leaderboard, new LeaderboardState(ui, unityGamingServices));
        stateMachine.Register(NewGame, new NewGameState(unityGamingServices, player, ui, stateMachine));
        
        await unityGamingServices.GetLeaderboards().RefreshScores();
        stateMachine.ChangeState(Leaderboard);
    }

    void Update()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void OnCollisionEnter(Collision collision)
    {
        stateMachine.OnCollisionEnter(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        stateMachine.OnCollisionExit(collision);
    }

    private EffectManager GetEffectManager()
    {
        return new EffectManager(Camera.main.GetComponent<CameraShake>());
    }
    
    private Player CreatePlayer()
    {
        return new Player(playerObject, 0);
    }
}