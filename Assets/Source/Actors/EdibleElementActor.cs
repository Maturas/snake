namespace Snake.Actors
{
    public abstract class EdibleElementActor : GameActor
    {
        public abstract void OnEaten(SnakeActor snake);
    }
}