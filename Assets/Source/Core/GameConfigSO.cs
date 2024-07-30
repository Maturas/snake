using System;
using Snake.Actors;
using Snake.Actors.EdibleElements;
using Snake.Board;
using UnityEngine;

namespace Snake.Core
{
    /// <summary>
    ///     Main game's configuration file, contains settings for the game
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Snake/GameConfig")]
    public class GameConfigSO : ScriptableObject
    {
        [SerializeField] private float tickInterval = 1.0f;
        [SerializeField] private int boardSize = 5;
        [SerializeField] private float cameraSize = 5.0f;
        [SerializeField] private float boardSpacing = 1.0f;
        [SerializeField] private int edibleElementsSpawnFrequency = 10;
        [SerializeField] private int edibleElementsSpawnAmount = 2;
        [SerializeField] private bool allowBoardLooping = true;
        [SerializeField] private GameField gameFieldPrefab;
        [SerializeField] private SnakeHeadActor snakeHeadPrefab;
        [SerializeField] private SnakeTailActor snakeTailPrefab;
        [SerializeField] private EdibleElementConfig[] edibleElementPrefabs;

        public float TickInterval => tickInterval;
        public int BoardSize => boardSize;
        public float CameraSize => cameraSize;
        public float BoardSpacing => boardSpacing;
        public int EdibleElementsSpawnFrequency => edibleElementsSpawnFrequency;
        public int EdibleElementsSpawnAmount => edibleElementsSpawnAmount;
        public bool AllowBoardLooping => allowBoardLooping;
        public GameField GameFieldPrefab => gameFieldPrefab;
        public SnakeHeadActor SnakeHeadPrefab => snakeHeadPrefab;
        public SnakeTailActor SnakeTailPrefab => snakeTailPrefab;
        public EdibleElementConfig[] EdibleElementPrefabs => edibleElementPrefabs;

        [Serializable]
        public class EdibleElementConfig
        {
            public EdibleElementActor Prefab;
            public int SpawnRarity;
        }
    }
}