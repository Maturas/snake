using Snake.Board;
using Snake.Core;
using UnityEngine;

namespace Snake.Actors
{
    public abstract class GameActor : MonoBehaviour
    {
        public GameField CurrentField { get; set; }
        
        public virtual void OnSpawn()
        {
        }

        public virtual void OnTick()
        {
        }
        
        public virtual void OnDespawn()
        {
        }
        
        public virtual void OnCollide(GameActor other)
        {
            if (other is SnakeHeadActor)
            {
                GameManager.Instance.GameOver();
            }
        }

        public void Despawn()
        {
            CurrentField.UnsetActor();
            CurrentField = null;
            OnDespawn();
            Destroy(gameObject); // TODO Object pooling
        }
    }
}