using System;
using Snake.Board;
using Snake.Input;
using Snake.UI;
using UnityEngine;

namespace Snake.Core
{
    // TODO Implement object pooling for GameFields and GameActors

    /// <summary>
    ///     Main class of the game, runs the game loop and handles game state
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameConfigSO gameConfig;
        [SerializeField] private GameBoard gameBoard;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Camera mainCamera;

        private int ediblesEaten;

        private bool isGameRunning;

        private float lastTickTime;

        private float tickIntervalMultiplier = 1.0f;
        private float tickIntervalMultiplierDuration;

        private int ticksSinceLastEdibleSpawn;

        public static GameManager Instance { get; private set; }
        public GameConfigSO GameConfig => gameConfig;

        private void Awake()
        {
            Instance = this;
            PlayerInputController.OnEnterPressed += OnEnterPressed;
            mainCamera.orthographicSize = gameConfig.CameraSize;
            uiManager.Initialize();
        }

        private void Update()
        {
            if (!isGameRunning)
            {
                return;
            }

            if (tickIntervalMultiplierDuration > 0.0f)
            {
                tickIntervalMultiplierDuration -= Time.deltaTime;
            }
            else
            {
                tickIntervalMultiplier = 1.0f;
            }

            if (Time.time - lastTickTime >= gameConfig.TickInterval * tickIntervalMultiplier)
            {
                lastTickTime = Time.time;
                OnTick();
            }
        }

        private void OnDestroy()
        {
            Instance = null;
            PlayerInputController.OnEnterPressed -= OnEnterPressed;
        }

        public static event Action OnGameStart;
        public static event Action OnGameOver;
        public static event Action OnGameWon;
        public static event Action<int> OnEdibleEaten;

        private void StartGame()
        {
            tickIntervalMultiplier = 1.0f;
            tickIntervalMultiplierDuration = 0.0f;
            ediblesEaten = 0;

            gameBoard.OnStartGame();

            lastTickTime = Time.time;
            isGameRunning = true;
            OnGameStart?.Invoke();
        }

        public void GameOver()
        {
            isGameRunning = false;
            OnGameOver?.Invoke();
        }

        public void GameWon()
        {
            isGameRunning = false;
            OnGameWon?.Invoke();
        }

        private void OnTick()
        {
            gameBoard.OnTick();

            ticksSinceLastEdibleSpawn++;
            if (ticksSinceLastEdibleSpawn >= gameConfig.EdibleElementsSpawnFrequency)
            {
                ticksSinceLastEdibleSpawn = 0;
                gameBoard.SpawnEdibles();
            }
        }

        private void OnEnterPressed()
        {
            if (isGameRunning)
            {
                return;
            }

            StartGame();
        }

        public void SetTickIntervalMultiplier(float multiplier, float duration)
        {
            tickIntervalMultiplier = multiplier;
            tickIntervalMultiplierDuration = duration;
        }

        public void OnSnakeEatEdible()
        {
            ediblesEaten++;
            OnEdibleEaten?.Invoke(ediblesEaten);
        }
    }
}