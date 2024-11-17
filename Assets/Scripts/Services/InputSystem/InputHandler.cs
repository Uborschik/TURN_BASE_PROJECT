using System;
using UnityEngine.InputSystem;

namespace Game.Services
{
    public sealed class InputHandler
    {
        private readonly Controls inputActions;

        public InputAction Click => inputActions.Gameplay.Click;

        public InputHandler()
        {
            inputActions = new();
            inputActions.Gameplay.Enable();
        }
    }
}