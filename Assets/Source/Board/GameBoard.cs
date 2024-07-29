using UnityEngine;

namespace Snake.Board
{
    public class GameBoard : MonoBehaviour
    {
        public GameField[,] Fields { get; private set; }
    }
}