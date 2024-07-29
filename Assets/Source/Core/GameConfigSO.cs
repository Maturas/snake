using System;
using Snake.Actors;
using Snake.Actors.EdibleElements;
using Snake.Board;
using UnityEngine;

namespace Snake.Core
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Snake/GameConfig")]
    public class GameConfigSO : ScriptableObject
    {
        [SerializeField]
        private float tickInterval = 1.0f;
        public float TickInterval => tickInterval;

        [SerializeField]
        private int boardSize = 5;
        public int BoardSize => boardSize;
        
        [SerializeField]
        private float boardSpacing = 1.0f;
        public float BoardSpacing => boardSpacing;

        [SerializeField]
        private int edibleElementsSpawnFrequency = 10;
        public int EdibleElementsSpawnFrequency => edibleElementsSpawnFrequency;

        [SerializeField]
        private int edibleElementsSpawnAmount = 2;
        public int EdibleElementsSpawnAmount => edibleElementsSpawnAmount;

        [SerializeField]
        private bool allowBoardLooping = true;
        public bool AllowBoardLooping => allowBoardLooping;

        [SerializeField] 
        private GameField gameFieldPrefab;
        public GameField GameFieldPrefab => gameFieldPrefab;

        [SerializeField]
        private SnakeHeadActor snakeHeadPrefab;
        public SnakeHeadActor SnakeHeadPrefab => snakeHeadPrefab;
        
        [SerializeField]
        private SnakeTailActor snakeTailPrefab;
        public SnakeTailActor SnakeTailPrefab => snakeTailPrefab;

        [SerializeField]
        private EdibleElementConfig[] edibleElementPrefabs;
        public EdibleElementConfig[] EdibleElementPrefabs => edibleElementPrefabs;

        [Serializable]
        public class EdibleElementConfig
        {
            public EdibleElementActor Prefab;

            [Range(0.0f, 1.0f)]
            public float SpawnRarity;
        }
    }
}