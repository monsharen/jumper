using System.Linq;
using UnityEngine;

namespace State
{
    public class CollisionUtil
    {
        public static bool IsCollisionWithWall(Collision collision)
        {
            return collision.contacts.Select(contact => contact.normal).Select(normal => !(normal.y > 0.7)).FirstOrDefault();
        }
    }
}