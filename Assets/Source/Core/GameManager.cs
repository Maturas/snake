using System;
using Snake.Board;
using Snake.Input;
using Snake.UI;
using UnityEngine;

namespace Snake.Core
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnGameStart;
        public static event Action OnGameOver;
        public static event Action<int> OnEdibleEaten;
        
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private GameConfigSO gameConfig;
        public GameConfigSO GameConfig => gameConfig;
        
        [SerializeField]
        private GameBoard gameBoard;
        public GameBoard GameBoard => gameBoard;
        
        [SerializeField]
        private UIManager uiManager;
        public UIManager UIManager => uiManager;
        
        private bool isGameRunning;
        
        private float lastTickTime;

        private float tickIntervalMultiplier = 1.0f;
        private float tickIntervalMultiplierDuration;
        
        private int ediblesEaten;
        
        private void Awake()
        {
            Instance = this;
            PlayerInputController.OnEnterPressed += OnEnterPressed;
            UIManager.Initialize();
        }

        private void OnDestroy()
        {
            Instance = null;
            PlayerInputController.OnEnterPressed -= OnEnterPressed;
        }

        private void Update()
        {
            if (!isGameRunning)
                return;
            
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

        private void StartGame()
        {
            tickIntervalMultiplier = 1.0f;
            tickIntervalMultiplierDuration = 0.0f;
            ediblesEaten = 0;
            
            GameBoard.Cleanup();
            GameBoard.Initialize();
            
            // TODO Spawn snake
            isGameRunning = true;
            OnGameStart?.Invoke();
        }

        public void GameOver()
        {
            isGameRunning = false;
            OnGameOver?.Invoke();
        }

        private void OnTick()
        {
            GameBoard.OnTick();
            // TODO spawn edible elements
        }

        private void OnEnterPressed()
        {
            if (isGameRunning)
                return;
            
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