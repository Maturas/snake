using Snake.Core;
using UnityEngine;

namespace Snake.Board
{
    public class GameBoard : MonoBehaviour
    {
        public GameField[,] Fields { get; private set; }

        public void Initialize()
        {
            var fieldPrefab = GameManager.Instance.GameConfig.GameFieldPrefab;
            var boardSize = GameManager.Instance.GameConfig.BoardSize;
            var boardSpacing = GameManager.Instance.GameConfig.BoardSpacing;
            
            Fields = new GameField[boardSize, boardSize];
            
            for (var x = 0; x < boardSize; x++)
            {
                for (var y = 0; y < boardSize; y++)
                {
                    var field = Instantiate(fieldPrefab, new Vector3(x * boardSpacing, y * boardSpacing, 0), Quaternion.identity, transform);
                    field.Init(new Vector2Int(x, y), this);
                }
            }
        }

        public void Cleanup()
        {
            if (Fields == null)
                return;
            
            foreach (var field in Fields)
            {
                if (field.CurrentActor != null)
                {
                    field.CurrentActor.Despawn();
                }
                Destroy(field);
            }
            
            Fields = null;
        }

        public void OnTick()
        {
            for (var x = 0; x < Fields.GetLength(0); x++)
            {
                for (var y = 0; y < Fields.GetLength(1); y++)
                {
                    var field = Fields[x, y];
                    if (field.CurrentActor != null)
                    {
                        field.CurrentActor.OnTick();
                    }
                }
            }
        }
    }
}