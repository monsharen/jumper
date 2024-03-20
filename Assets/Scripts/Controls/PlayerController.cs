using UnityEngine;

namespace Controls
{
    public class PlayerController : MonoBehaviour
    {

        public Game game;
    
        public void OnCollisionEnter(Collision collision)
        {
            game.OnCollisionEnter(collision);
        }

        public void OnCollisionExit(Collision collision)
        {
            game.OnCollisionExit(collision);
        }
    }
}
