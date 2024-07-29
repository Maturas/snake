using Snake.Input;
using UnityEngine;

namespace Snake.Actors
{
    public class SnakeActor : GameActor
    {
        private Vector2Int currentDirection;
        
        public override void OnSpawn()
        {
            base.OnSpawn();
            PlayerInputController.OnMovementChanged += OnMovementChanged;
            
            SetDirection(Vector2Int.up);
        }
        
        public override void OnDespawn()
        {
            base.OnDespawn();
            PlayerInputController.OnMovementChanged -= OnMovementChanged;
        }
        
        private void SetDirection(Vector2Int direction)
        {
            currentDirection = direction;
            transform.up = new Vector3(direction.x, direction.y, 0.0f);
        }
        
        private void OnMovementChanged(Vector2Int direction)
        {
            if (direction == -currentDirection)
                return;
            
            SetDirection(direction);
        }
    }
}