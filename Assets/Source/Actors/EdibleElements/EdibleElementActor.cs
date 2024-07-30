using Snake.Core;

namespace Snake.Actors.EdibleElements
{
    /// <summary>
    ///     Base class for all elements that can be eaten by the snake
    /// </summary>
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