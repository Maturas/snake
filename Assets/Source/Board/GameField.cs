using System.Collections.Generic;
using Snake.Actors;
using Snake.Core;
using UnityEngine;

namespace Snake.Board
{
    /// <summary>
    ///     Game field class, represents a single field on the game board, can be occupied by a game actor
    /// </summary>
    public class GameField : MonoBehaviour
    {
        public Vector2Int Position { get; private set; }
        public GameActor CurrentActor { get; private set; }
        public GameBoard GameBoard { get; private set; }
        
        public bool IsOccupied => CurrentActor != null;
        
        public void Init(Vector2Int position, GameBoard gameBoard)
        {
            Position = position;
            GameBoard = gameBoard;
        }

        public bool TrySetActor(GameActor actor)
        {
            if (IsOccupied)
            {
                CurrentActor.OnCollide(actor);
            }
            
            if (CurrentActor == null)
            {
                if (actor.CurrentField != null)
                {
                    actor.CurrentField.UnsetActor();
                }
                
                CurrentActor = actor;
                actor.CurrentField = this;
                actor.transform.position = new Vector3(transform.position.x, transform.position.y, actor.transform.position.z);
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UnsetActor()
        {
            CurrentActor = null;
        }
        
        public GameField GetAdjacent(Vector2Int direction)
        {
            var targetPosition = Position + direction;
            
            if (targetPosition.x > 0 && targetPosition.x < GameBoard.BoardSize &&
                targetPosition.y > 0 && targetPosition.y < GameBoard.BoardSize)
            {
                return GameBoard.GetField(targetPosition);
            }
            else if (GameManager.Instance.GameConfig.AllowBoardLooping)
            {
                if (targetPosition.x < 0)
                    targetPosition.x = GameBoard.BoardSize - 1;
                else if (targetPosition.x >= GameBoard.BoardSize)
                    targetPosition.x = 0;
                
                if (targetPosition.y < 0)
                    targetPosition.y = GameBoard.BoardSize - 1;
                else if (targetPosition.y >= GameBoard.BoardSize)
                    targetPosition.y = 0;
                
                return GameBoard.GetField(targetPosition);
            }
            else return null;
        }

        public GameField[] GetAdjacents(bool includeDiagonals)
        {
            var adjacents = new List<GameField>();
            var directions = new List<Vector2Int>
            {
                Vector2Int.up,
                Vector2Int.down,
                Vector2Int.left,
                Vector2Int.right
            };

            if (includeDiagonals)
            {
                directions.AddRange(new[]
                {
                    new Vector2Int(1, 1),
                    new Vector2Int(1, -1),
                    new Vector2Int(-1, -1),
                    new Vector2Int(-1, 1)
                });
            }

            foreach (var direction in directions)
            {
                var targetPosition = Position + direction;

                if (targetPosition.x >= 0 && targetPosition.x < GameBoard.BoardSize &&
                    targetPosition.y >= 0 && targetPosition.y < GameBoard.BoardSize)
                {
                    adjacents.Add(GameBoard.GetField(targetPosition));
                }
                else if (GameManager.Instance.GameConfig.AllowBoardLooping)
                {
                    if (targetPosition.x < 0)
                        targetPosition.x = GameBoard.BoardSize - 1;
                    else if (targetPosition.x >= GameBoard.BoardSize)
                        targetPosition.x = 0;

                    if (targetPosition.y < 0)
                        targetPosition.y = GameBoard.BoardSize - 1;
                    else if (targetPosition.y >= GameBoard.BoardSize)
                        targetPosition.y = 0;

                    adjacents.Add(GameBoard.GetField(targetPosition));
                }
            }

            return adjacents.ToArray();
        }
    }
}