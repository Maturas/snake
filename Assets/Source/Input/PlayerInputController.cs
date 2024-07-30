using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snake.Input
{
    /// <summary>
    ///     Input controller for the player of the game of Snake
    /// </summary>
    public class PlayerInputController : InputControllerBase
    {
        // TODO refactor events to non-static to allow local multiplayer
        public static event Action<Vector2Int> OnMovementChanged;
        public static event Action OnEnterPressed;

        private void OnMovement(InputValue value)
        {
            if (ShouldIgnoreInput)
            {
                return;
            }

            var valueVector = value.Get<Vector2>();

            if (valueVector == Vector2.zero)
            {
                return;
            }

            var direction = new Vector2Int((int) valueVector.x, (int) valueVector.y);
            if (direction.x != 0 && direction.y != 0)
            {
                if (Mathf.Abs(valueVector.x) > Mathf.Abs(valueVector.y))
                {
                    direction.y = 0;
                }
                else
                {
                    direction.x = 0;
                }
            }

            OnMovementChanged?.Invoke(direction);
        }

        private void OnEnter(InputValue value)
        {
            if (ShouldIgnoreInput)
            {
                return;
            }

            if (!value.isPressed)
            {
                return;
            }

            OnEnterPressed?.Invoke();
        }
    }
}