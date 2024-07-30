using System.Collections.Generic;
using Snake.Board;
using Snake.Core;
using Snake.Input;
using UnityEngine;

namespace Snake.Actors
{
    // TODO refactor schedules into a generic queue of actions
    
    /// <summary>
    ///     Snake's head actor, main controller for the player
    /// </summary>
    public class SnakeHeadActor : GameActor
    {
        [SerializeField]
        private int initialTailLength;
        
        private Vector2Int currentDirection;
        private List<SnakeTailActor> tail;
        
        private int scheduledTailLengthChange;
        private bool scheduledReverse;
        
        public override void OnSpawn()
        {
            PlayerInputController.OnMovementChanged += OnMovementChanged;
            
            SetDirection(Vector2Int.up);
            ChangeTailLength(initialTailLength);
        }
        
        public override void OnDespawn()
        {
            PlayerInputController.OnMovementChanged -= OnMovementChanged;
        }

        public override void OnTick()
        {
            var oldField = CurrentField;
            var targetField = CurrentField.GetAdjacent(currentDirection);
            
            if (targetField == null || !targetField.TrySetActor(this))
            {
                // Hit an obstacle, game over
                GameManager.Instance.GameOver();
            }
            else
            {
                // Change tail length if necessary (can be scheduled by an edible element)
                if (scheduledTailLengthChange != 0)
                {
                    ChangeTailLength(scheduledTailLengthChange);
                    scheduledTailLengthChange = 0;
                }
                
                // Update the tail's position according to head's movemement
                MoveTail(oldField);
                
                // Reverse snake if necessary (can be scheduled by an edible element)
                if (scheduledReverse)
                {
                    ReverseSnake();
                    scheduledReverse = false;
                }
            }
        }

        private void MoveTail(GameField previousHeadPosition)
        {
            if (tail.Count == 0)
                return;
            
            var previousTailPosition = previousHeadPosition;
            foreach (var tailActor in tail)
            {
                var temp = tailActor.CurrentField;
                tailActor.CurrentField.UnsetActor();
                previousTailPosition.TrySetActor(tailActor);
                previousTailPosition = temp;
            }
        }

        /// <summary>
        ///     Schedules a change in the snake's tail length at the end of the current tick
        /// </summary>
        /// <param name="delta"></param>
        public void ScheduleTailLength(int delta)
        {
            scheduledTailLengthChange = delta;
        }
        
        /// <summary>
        ///     Schedules a reverse of the snake at the end of the current tick
        /// </summary>
        public void ScheduleReverseSnake()
        {
            scheduledReverse = true;
        }

        private void ChangeTailLength(int delta)
        {
            tail ??= new List<SnakeTailActor>();
            switch (delta)
            {
                case 0:
                    return;
                case > 0:
                    GrowTail(delta);
                    break;
                default:
                    ShrinkTail(-delta);
                    break;
            }

            // If the snake occupies the entire game board, the game is won
            if (tail.Count == CurrentField.GameBoard.BoardSize * CurrentField.GameBoard.BoardSize - 1)
            {
                GameManager.Instance.GameWon();
            }
        }

        private void GrowTail(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                Vector2Int targetPosition;
                GameActor lastTailSegment;

                switch (tail.Count)
                {
                    case 0:
                    {
                        // Snake has no tail, spawn a new segment behind the head
                        lastTailSegment = this;
                        targetPosition = CurrentField.Position - currentDirection;
                        break;
                    }
                    
                    case 1:
                    {
                        // Snake has one tail segment, the new segment's position is based on the relative position of the head and the tail
                        lastTailSegment = tail[^1];
                        var headField = CurrentField;
                        targetPosition = headField.Position - (headField.Position - lastTailSegment.CurrentField.Position);
                        break;
                    }
                    
                    default:
                    {
                        // Snake has multiple tail segments, the new segment's position is based on the relative position of the last two tail segments
                        lastTailSegment = tail[^1];
                        var secondLastTailField = tail[^2].CurrentField;
                        targetPosition = lastTailSegment.CurrentField.Position -
                                         (lastTailSegment.CurrentField.Position - secondLastTailField.Position);
                        break;
                    }
                }

                var targetField = CurrentField.GameBoard.GetField(targetPosition);
                if (targetField.IsOccupied)
                {
                    // If the target field is occupied, try to find an adjacent field that is not occupied
                    var adjacentFields = lastTailSegment.CurrentField.GetAdjacents(false);
                    foreach (var field in adjacentFields)
                    {
                        if (!field.IsOccupied)
                        {
                            targetField = field;
                            break;
                        }
                    }
                }

                // Spawn the new tail segment if the target field is not occupied
                if (!targetField.IsOccupied)
                {
                    tail.Add(CurrentField.GameBoard.SpawnSnakeTailSegment(targetField));
                }
            }
        }

        private void ShrinkTail(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                if (tail.Count == 0)
                {
                    // Snake has lost its entire tail, game over
                    GameManager.Instance.GameOver();
                    return;
                }
                    
                tail[^1].Despawn();
                tail.RemoveAt(tail.Count - 1);
            }
        }
        
        private void ReverseSnake()
        {
            if (tail.Count == 0)
                return;
            
            // Reverse the tail's order
            var lastTail = tail[^1];
            tail.RemoveAt(tail.Count - 1);
            tail.Reverse();
            tail.Add(lastTail);

            // Swap the head and last tail segment's positions
            var oldTailPosition = lastTail.CurrentField;
            var oldHeadPosition = CurrentField;
            
            oldTailPosition.UnsetActor();
            oldHeadPosition.UnsetActor();
            CurrentField = null;
            lastTail.CurrentField = null;
            
            oldTailPosition.TrySetActor(this);
            oldHeadPosition.TrySetActor(lastTail);
            
            // Reverse the movement direction
            SetDirection(-currentDirection);
        }

        private void SetDirection(Vector2Int direction)
        {
            currentDirection = direction;
            
            // Rotate the head to face the new direction
            transform.up = new Vector3(direction.x, direction.y, 0.0f);
        }
        
        private void OnMovementChanged(Vector2Int direction)
        {
            // Prevent the snake from moving into itself
            if (tail.Count > 0)
            {
                var firstTail = tail[0];
                if (firstTail.CurrentField.Position == CurrentField.Position + direction)
                    return;
            }
            
            SetDirection(direction);
        }
    }
}