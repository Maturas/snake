using Snake.Core;
using UnityEngine;

namespace Snake.Actors.EdibleElements
{
    /// <summary>
    ///     Element that temporarily changes the game's pace when eaten
    /// </summary>
    public class EdibleElementGamePaceActor : EdibleElementActor
    {
        [SerializeField, Range(0.1f, 5.0f)] 
        private float speedMultiplier = 2.0f;
        
        [SerializeField, Range(5.0f, 20.0f)]
        private float multiplierDuration = 5.0f;
        
        protected override void OnEaten(SnakeHeadActor snakeHead)
        {
            GameManager.Instance.SetTickIntervalMultiplier(speedMultiplier, multiplierDuration);
        }
    }
}