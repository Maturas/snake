using Snake.Board;
using Snake.Input;
using Snake.UI;
using UnityEngine;

namespace Snake.Core
{
    public class GameManager : MonoBehaviour
    {
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
        
        private bool isGameRunning = false;
        
        private float lastTickTime = 0.0f;

        private float tickIntervalMultiplier = 1.0f;
        private float tickIntervalMultiplierDuration = 0.0f;
        
        private void Awake()
        {
            Instance = this;
            PlayerInputController.OnEnterPressed += OnEnterPressed;
            
            // TODO UI
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
            GameBoard.Cleanup();
            GameBoard.Initialize();
            // TODO Spawn snake
            // TODO UI
            isGameRunning = true;
        }

        public void GameOver()
        {
            // TODO UI
            isGameRunning = false;
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
    }
}