using System.Collections.Generic;
using System.Linq;
using Snake.Actors;
using Snake.Core;
using UnityEngine;

namespace Snake.Board
{
    /// <summary>
    ///     Game board class, handles game fields and game actors
    /// </summary>
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] 
        private float fieldZ;
        
        [SerializeField] 
        private float actorZ;
        
        public int BoardSize { get; private set; }
        
        private GameField[,] fields { get; set; }

        /// <summary>
        ///     Starts a new game
        /// </summary>
        public void OnStartGame()
        {
            Cleanup();
            Initialize();
            SpawnSnake();
            SpawnEdibles();
        }
        
        /// <summary>
        ///     Returns the game field at a given position (if exists)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public GameField GetField(Vector2Int position)
        {
            if (position.x >= 0 && position.x < BoardSize &&
                position.y >= 0 && position.y < BoardSize)
            {
                return fields[position.x, position.y];
            }

            return null;
        }

        private void Initialize()
        {
            BoardSize = GameManager.Instance.GameConfig.BoardSize;

            var fieldPrefab = GameManager.Instance.GameConfig.GameFieldPrefab;
            var boardSpacing = GameManager.Instance.GameConfig.BoardSpacing;

            fields = new GameField[BoardSize, BoardSize];

            // Calculate the offset to center the board at (0, 0, 0)
            var offset = new Vector3((BoardSize - 1) * boardSpacing / 2, (BoardSize - 1) * boardSpacing / 2, fieldZ);

            for (var x = 0; x < BoardSize; x++)
            {
                for (var y = 0; y < BoardSize; y++)
                {
                    var position = new Vector3(x * boardSpacing, y * boardSpacing, fieldZ) - offset;
                    var field = Instantiate(fieldPrefab, position, Quaternion.identity, transform);
                    field.Init(new Vector2Int(x, y), this);
                    fields[x, y] = field;
                }
            }
        }

        private void Cleanup()
        {
            if (fields == null)
                return;
            
            foreach (var field in fields)
            {
                if (field.CurrentActor != null)
                {
                    field.CurrentActor.Despawn();
                }
                Destroy(field);
            }
            
            fields = null;
        }

        /// <summary>
        ///     Called on every game tick
        /// </summary>
        public void OnTick()
        {
            var actors = new List<GameActor>();
            for (var x = 0; x < BoardSize; x++)
            {
                for (var y = 0; y < BoardSize; y++)
                {
                    var field = fields[x, y];
                    if (field.CurrentActor != null)
                    {
                        actors.Add(field.CurrentActor);
                    }
                }
            }
            
            foreach (var actor in actors)
            {
                actor.OnTick();
            }
        }

        private List<GameField> GetUnoccupiedFields()
        {
            var unoccupiedFields = new List<GameField>();

            for (var x = 0; x < BoardSize; x++)
            {
                for (var y = 0; y < BoardSize; y++)
                {
                    var field = fields[x, y];
                    if (!field.IsOccupied)
                    {
                        unoccupiedFields.Add(field);
                    }
                }
            }

            return unoccupiedFields;
        }

        private GameField GetCenterField()
        {
            var centerX = BoardSize / 2;
            var centerY = BoardSize / 2;
            return fields[centerX, centerY];
        }

        /// <summary>
        ///     Spawns edible elements on the board according to GameConfig's settings
        /// </summary>
        public void SpawnEdibles()
        {
            var configs = GameManager.Instance.GameConfig.EdibleElementPrefabs;
            if (configs.Length == 0)
                return;
            
            var unoccupiedFields = GetUnoccupiedFields();
            var amount = Mathf.Min(GameManager.Instance.GameConfig.EdibleElementsSpawnAmount, unoccupiedFields.Count);
            
            var selectedConfigs = configs.PickRandomWeightedWithDuplicates(x => x.SpawnRarity, amount).ToArray();
            var selectedFields = unoccupiedFields.PickRandom(amount).ToArray();
            
            for (var i = 0; i < amount; i++)
            {
                SpawnActor(selectedConfigs[i].Prefab, selectedFields[i]);
            }
        }

        private void SpawnSnake()
        {
            SpawnActor(GameManager.Instance.GameConfig.SnakeHeadPrefab, GetCenterField());
        }
        
        public SnakeTailActor SpawnSnakeTailSegment(GameField targetField)
        {
            return SpawnActor(GameManager.Instance.GameConfig.SnakeTailPrefab, targetField);
        }

        private T SpawnActor<T>(T prefab, GameField field) where T : GameActor
        {
            var fieldPosition = field.transform.position;
            fieldPosition.z = actorZ;
            
            var actor = Instantiate(prefab.gameObject, fieldPosition, Quaternion.identity).GetComponent<T>();
            field.TrySetActor(actor);
            actor.OnSpawn();

            return actor;
        }
    }
}