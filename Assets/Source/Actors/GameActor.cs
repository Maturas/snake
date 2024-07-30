using Snake.Board;
using Snake.Core;
using UnityEngine;

namespace Snake.Actors
{
    /// <summary>
    ///     Base class for all game actors, that can be spawned on a game field
    /// </summary>
    public abstract class GameActor : MonoBehaviour
    {
        public GameField CurrentField { get; set; }
        
        /// <summary>
        ///     Invoked when the actor is spawned on a game field
        /// </summary>
        public virtual void OnSpawn()
        {
        }

        /// <summary>
        ///     Invoked on every game tick
        /// </summary>
        public virtual void OnTick()
        {
        }
        
        /// <summary>
        ///     Invoked when the actor is despawned from a game field
        /// </summary>
        public virtual void OnDespawn()
        {
        }
        
        /// <summary>
        ///     Invoked when the actor collides with another actor (by the actor attempting to move to this actor's field)
        /// </summary>
        public virtual void OnCollide(GameActor other)
        {
            if (other is SnakeHeadActor)
            {
                GameManager.Instance.GameOver();
            }
        }

        /// <summary>
        ///     Removes the actor from the game field and destroys its game object
        /// </summary>
        public void Despawn()
        {
            CurrentField.UnsetActor();
            CurrentField = null;
            OnDespawn();
            Destroy(gameObject);
        }
    }
}