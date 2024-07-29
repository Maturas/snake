using System;
using Snake.Actors;
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
        private int initialSnakeLength = 3;
        public int InitialSnakeLength => initialSnakeLength;

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
        private SnakeActor snakePrefab;
        public SnakeActor SnakePrefab => snakePrefab;

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