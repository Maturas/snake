using Snake.Core;
using TMPro;
using UnityEngine;

namespace Snake.UI
{
    /// <summary>
    ///     UI manager class, handles UI screens and elements
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject startGameScreen;
        
        [SerializeField]
        private GameObject gameOverScreen;
        
        [SerializeField]
        private GameObject gameWonScreen;
        
        [SerializeField]
        private GameObject inGameScreen;

        [SerializeField] 
        private TextMeshProUGUI eatenEdiblesCounter;

        public void Initialize()
        {
            GameManager.OnGameStart += OnGameStart;
            GameManager.OnGameOver += OnGameOver;
            GameManager.OnGameWon += OnGameWon;
            GameManager.OnEdibleEaten += OnEdibleEaten;
            
            OnGameInitialize();
        }

        private void OnDestroy()
        {
            GameManager.OnGameStart -= OnGameStart;
            GameManager.OnGameOver -= OnGameOver;
            GameManager.OnGameWon -= OnGameWon;
            GameManager.OnEdibleEaten -= OnEdibleEaten;
        }

        private void OnGameInitialize()
        {
            startGameScreen.SetActive(true);
            gameOverScreen.SetActive(false);
            gameWonScreen.SetActive(false);
            inGameScreen.SetActive(false);
        }
        
        private void OnGameStart()
        {
            startGameScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            gameWonScreen.SetActive(false);
            inGameScreen.SetActive(true);
            
            eatenEdiblesCounter.text = "0";
        }
        
        private void OnGameOver()
        {
            startGameScreen.SetActive(false);
            gameOverScreen.SetActive(true);
            gameWonScreen.SetActive(false);
            inGameScreen.SetActive(false);
        }
        
        private void OnGameWon()
        {
            startGameScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            gameWonScreen.SetActive(true);
            inGameScreen.SetActive(false);
        }
        
        private void OnEdibleEaten(int eatenEdibles)
        {
            eatenEdiblesCounter.text = eatenEdibles.ToString();
        }
    }
}