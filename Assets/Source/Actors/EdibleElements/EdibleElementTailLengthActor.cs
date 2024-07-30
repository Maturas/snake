using UnityEngine;

namespace Snake.Actors.EdibleElements
{
    /// <summary>
    ///     Element that changes the snake's tail length when eaten
    /// </summary>
    public class EdibleElementTailLengthActor : EdibleElementActor
    {
        [SerializeField]
        private int tailLengthDelta = 1;
        
        protected override void OnEaten(SnakeHeadActor snakeHead)
        {
            snakeHead.ScheduleTailLength(tailLengthDelta);
        }
    }
}