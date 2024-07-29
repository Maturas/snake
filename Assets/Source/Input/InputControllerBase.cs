using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Snake.Input
{
    /// <summary>
    ///     Base class for an InputController - a component that reads inputs from a single InputDevice
    /// </summary>
    public abstract class InputControllerBase : MonoBehaviour
    {
        /// <summary>
        ///     This controller's bound InputDevice
        /// </summary>
        public InputDevice BoundDevice { get; private set; }
        
        /// <summary>
        ///     Sets up this controller to work with given device
        /// </summary>
        /// <param name="device"></param>
        public void Setup(InputDevice device)
        {
            BoundDevice = device;
            string controllerName = $"PlayerInputController: {device.displayName} [{device.deviceId}]";
            gameObject.name = controllerName;
            
            Debug.Log($"Created {controllerName}");

            DontDestroyOnLoad(gameObject);
        }
        
        /// <summary>
        ///     Reselects the first preferred UI control
        /// </summary> 
        /// <returns></returns>
        protected static bool ReselectFirstUIControl()
        {
            var reselect = EventSystem.current.currentSelectedGameObject == null &&
                           EventSystem.current.firstSelectedGameObject != null;

            if (reselect)
                EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);

            return reselect;
        }

        /// <summary>
        ///     Returns true if input should be ignored
        /// </summary>
        protected virtual bool ShouldIgnoreInput => false;
    }
}