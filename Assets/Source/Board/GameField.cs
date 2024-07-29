using Snake.Actors;
using UnityEngine;

namespace Snake.Board
{
    public class GameField : MonoBehaviour
    {
        public Vector2Int Position { get; private set; }
        public GameActor CurrentActor { get; private set; }
    }
}