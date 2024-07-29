using System.Collections.Generic;
using Snake.Core;
using Snake.Input;
using UnityEngine;

namespace Snake.Actors
{
    public class SnakeHeadActor : GameActor
    {
        [SerializeField]
        private int initialTailLength;
        
        private Vector2Int currentDirection;
        private List<SnakeTailActor> tail;
        
        public override void OnSpawn()
        {
            base.OnSpawn();
            PlayerInputController.OnMovementChanged += OnMovementChanged;
            
            SetDirection(Vector2Int.up);
            ChangeTailLength(initialTailLength);
        }
        
        public override void OnDespawn()
        {
            base.OnDespawn();
            PlayerInputController.OnMovementChanged -= OnMovementChanged;
        }

        public override void OnTick()
        {
            var oldField = CurrentField;
            var targetField = CurrentField.GetAdjacent(currentDirection);
            if (targetField == null || !targetField.TrySetActor(this))
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                // TODO update tail
            }
        }

        public void ChangeTailLength(int delta)
        {
            // TODO change tail length
        }
        
        public void ReverseSnake()
        {
            // TODO
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