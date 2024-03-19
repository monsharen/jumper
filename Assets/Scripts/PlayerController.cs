using Controls;
using State;
using Ugs;
using UI;
using UnityEngine;
using UnityEngine.UIElements;
using static State.State;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public UIDocument uiDocument;
    public VisualTreeAsset leaderboardEntry;
    
    private float jumpStartTime;

    private StateMachine stateMachine;
    async void Start()
    {
        stateMachine = new StateMachine();
        var unityGamingServices = new UnityGamingServices(
            new Leaderboards(), new Authentication(), new Analytics(), new RemoteConfig());
        Debug.Log("Initialising UGS...");
        await unityGamingServices.Init();
        
        var cameraShake = Camera.main.GetComponent<CameraShake>();
        var effectManager = new EffectManager(cameraShake);
        var player = new Player(gameObject, unityGamingServices);
        var ui = new Ui(unityGamingServices, stateMachine, uiDocument, leaderboardEntry);
        stateMachine.Register(Dead, new DeadState(player, ui, effectManager, unityGamingServices, stateMachine));
        stateMachine.Register(Falling, new FallingState( player, unityGamingServices, effectManager, stateMachine));
        stateMachine.Register(Jumping, new JumpingState(player, effectManager, stateMachine));
        stateMachine.Register(Grounded, new GroundedState(player, effectManager, stateMachine));
        stateMachine.Register(Leaderboard, new LeaderboardState(ui, unityGamingServices));
        stateMachine.Register(NewGame, new NewGameState(player, stateMachine));
        
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

    private void OnCollisionEnter(Collision collision)
    {
        stateMachine.OnCollisionEnter(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        stateMachine.OnCollisionExit(collision);
    }
}