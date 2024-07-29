using UnityEngine;

namespace Snake.Actors.EdibleElements
{
    public class EdibleElementTailLengthActor : EdibleElementActor
    {
        [SerializeField]
        private int tailLengthDelta = 1;
        
        protected override void OnEaten(SnakeHeadActor snakeHead)
        {
            snakeHead.ChangeTailLength(tailLengthDelta);
        }
    }
}