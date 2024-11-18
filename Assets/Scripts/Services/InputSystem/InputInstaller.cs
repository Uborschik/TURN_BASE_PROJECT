using System;
using UnityEngine.InputSystem;

namespace Game.Services.Input
{
    public class InputInstaller
    {
        private readonly Controls inputActions;
        public InputAction Click => inputActions.Gameplay.LeftClick;
        public InputAction Escape => inputActions.Gameplay.Escape;

        public InputInstaller()
        {
            inputActions = new();
            inputActions.Gameplay.Enable();
        }
    }
}