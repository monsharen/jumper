using UnityEngine;

namespace State
{
    public interface IState
    {
        public void Start();
        public void Update();

        public void FixedUpdate();
        public void End();

        public void OnCollisionEnter(Collision collision);
        public void OnCollisionExit(Collision collision);
    }
}