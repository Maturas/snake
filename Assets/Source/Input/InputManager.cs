using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snake.Input
{
    /// <summary>
    ///     Handles all InputControllers
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [field: SerializeField]
        private InputControllerBase InputControllerPrefab { get; set; }
        
        /// <summary>
        ///     References to all existing InputControllers
        /// </summary>
        private List<InputControllerBase> AllControllers { get; set; }

        private void Awake()
        {
            AllControllers = new List<InputControllerBase>();
            
            // Setup Input Controllers for all available devices
            foreach (var device in InputSystem.devices)
            {
                CreateController(device);
            }
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void OnDestroy()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        /// <summary>
        ///     Handles input device mode changes
        /// </summary>
        /// <param name="device"></param>
        /// <param name="mode"></param>
        private void OnDeviceChange(InputDevice device, InputDeviceChange mode)
        {
            switch (mode)
            {
                case InputDeviceChange.Added:
                {
                    CreateController(device);
                    break;
                }
                case InputDeviceChange.Removed:
                {
                    RemoveController(device);
                    break;
                }
            }
        }
        
        /// <summary>
        ///     Creates a new InputController instance for given InputDevice
        /// </summary>
        /// <param name="device"></param>
        private void CreateController(InputDevice device)
        {
            if (device.displayName == "Mouse") // Mouse gets bound automatically with the keyboard
                return;
            
            var input = PlayerInput.Instantiate(InputControllerPrefab.gameObject, pairWithDevice: device);
            var controller = input.GetComponent<InputControllerBase>();
            controller.Setup(device);
            AllControllers.Add(controller);
        }
        
        /// <summary>
        ///     Removes the InputController to which given InputDevice is bound.
        /// </summary>
        /// <param name="device"></param>
        private void RemoveController(InputDevice device)
        {
            var controller = AllControllers.SingleOrDefault(i => i.BoundDevice.deviceId == device.deviceId);
            
            Debug.Assert(controller != null);
            AllControllers.Remove(controller);
            Destroy(controller.gameObject);
        }
    }
}