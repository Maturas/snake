using Snake.Core;

namespace Snake.Actors.EdibleElements
{
    public abstract class EdibleElementActor : GameActor
    {
        public override void OnCollide(GameActor other)
        {
            if (other is SnakeHeadActor snakeHead)
            {
                OnEaten(snakeHead);
                GameManager.Instance.OnSnakeEatEdible();
                Despawn();
            }
        }

        protected abstract void OnEaten(SnakeHeadActor snakeHead);
    }
}