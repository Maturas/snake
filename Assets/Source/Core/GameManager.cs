using Snake.Board;
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
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}