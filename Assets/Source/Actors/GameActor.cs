using UnityEngine;

namespace Snake.Actors
{
    public abstract class GameActor : MonoBehaviour
    {
        public virtual void OnSpawn()
        {
        }

        public virtual void OnTick()
        {
        }
        
        public virtual void OnDespawn()
        {
        }
    }
}