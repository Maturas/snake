namespace Snake.Actors.EdibleElements
{
    /// <summary>
    ///     Element that reverses the snake's direction when eaten
    /// </summary>
    public class EdibleElementSnakeReverseActor : EdibleElementActor
    {
        protected override void OnEaten(SnakeHeadActor snakeHead)
        {
            snakeHead.ScheduleReverseSnake();
        }
    }
}