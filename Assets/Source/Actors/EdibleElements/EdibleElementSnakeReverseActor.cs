namespace Snake.Actors.EdibleElements
{
    public class EdibleElementSnakeReverseActor : EdibleElementActor
    {
        protected override void OnEaten(SnakeHeadActor snakeHead)
        {
            snakeHead.ReverseSnake();
        }
    }
}