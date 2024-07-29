using Snake.Actors;
using Snake.Core;
using UnityEngine;

namespace Snake.Board
{
    public class GameField : MonoBehaviour
    {
        public Vector2Int Position { get; private set; }
        public GameActor CurrentActor { get; private set; }
        public GameBoard GameBoard { get; private set; }
        
        public void Init(Vector2Int position, GameBoard gameBoard)
        {
            Position = position;
            GameBoard = gameBoard;
        }

        public bool TrySetActor(GameActor actor)
        {
            if (CurrentActor != null)
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
            
            if (targetPosition.x > 0 && targetPosition.x < GameBoard.Fields.GetLength(0) &&
                targetPosition.y > 0 && targetPosition.y < GameBoard.Fields.GetLength(1))
            {
                return GameBoard.Fields[targetPosition.x, targetPosition.y];
            }
            else if (GameManager.Instance.GameConfig.AllowBoardLooping)
            {
                if (targetPosition.x < 0)
                    targetPosition.x = GameBoard.Fields.GetLength(0) - 1;
                else if (targetPosition.x >= GameBoard.Fields.GetLength(0))
                    targetPosition.x = 0;
                
                if (targetPosition.y < 0)
                    targetPosition.y = GameBoard.Fields.GetLength(1) - 1;
                else if (targetPosition.y >= GameBoard.Fields.GetLength(1))
                    targetPosition.y = 0;
                
                return GameBoard.Fields[targetPosition.x, targetPosition.y];
            }
            else return null;
        }
    }
}