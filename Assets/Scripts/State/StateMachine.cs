using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class StateMachine
    {
        private Dictionary<State, IState> states = new Dictionary<State, IState>();
        private IState currentState;
        private IState nextState;

        public StateMachine()
        {
            currentState = new EmptyState();
            nextState = currentState;
        }

        public void ChangeState(State stateId)
        {
            var state = states[stateId];
            Debug.Log("will change state to " + state);
            nextState = state;
        }

        public void Register(State stateId, IState state)
        {
            states[stateId] = state;
        }

        public void Update()
        {
            if (currentState != nextState)
            {
                Debug.Log("switching from " + currentState + " to " + nextState);
                currentState.End();
                currentState = nextState;
                currentState.Start();
            }
            
            currentState.Update();
        }
        
        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
        
        public void OnCollisionEnter(Collision collision)
        {
            currentState.OnCollisionEnter(collision);
        }

        public void OnCollisionExit(Collision collision)
        {
            currentState.OnCollisionExit(collision);
        }
    }

    class EmptyState : IState
    {
        public void Start()
        {
            
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