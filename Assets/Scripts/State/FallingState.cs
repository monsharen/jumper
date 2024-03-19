using Ugs;
using UnityEngine;

namespace State
{
    public class FallingState : IState
    {

        //private readonly float fallingSpeed = -20f;
        private readonly StateMachine stateMachine;
        private readonly UnityGamingServices unityGamingServices;
        private readonly EffectManager effectManager;
        private readonly Player player;
        
        private float gravity = 0f;
        private float maxFallSpeed = 0f;

        public FallingState(Player player, UnityGamingServices unityGamingServices, EffectManager effectManager, StateMachine stateMachine)
        {
            this.unityGamingServices = unityGamingServices;
            this.player = player;
            this.effectManager = effectManager;
            this.stateMachine = stateMachine;
        }

        public void Start()
        {
            gravity = unityGamingServices.GetRemoteConfig().Gravity;
            maxFallSpeed = unityGamingServices.GetRemoteConfig().MaxFallSpeed;
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            player.MoveForward();
            
            var fallSpeed = Mathf.Max(maxFallSpeed, gravity * Time.fixedDeltaTime);
            
            player.GetRigidbody().velocity = new Vector3(0, -fallSpeed, 0);
            
            if (player.GetRigidbody().transform.position.y < -1)
            {
                stateMachine.ChangeState(State.Dead);
            }
        }

        public void End()
        {
            
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (CollisionUtil.IsCollisionWithWall(collision))
            {
                stateMachine.ChangeState(State.Dead);
                return;
            }
            
            if (collision.gameObject.CompareTag("Steel"))
            {
                stateMachine.ChangeState(State.Grounded);
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            
        }
    }
}