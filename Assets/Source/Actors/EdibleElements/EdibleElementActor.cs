namespace Snake.Actors.EdibleElements
{
    public abstract class EdibleElementActor : GameActor
    {
        public override void OnCollide(GameActor other)
        {
            if (other is SnakeHeadActor snakeHead)
            {
                OnEaten(snakeHead);
                Despawn();
            }
        }

        protected abstract void OnEaten(SnakeHeadActor snakeHead);
    }
}