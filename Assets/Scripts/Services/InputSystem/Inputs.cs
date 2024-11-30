using UnityEngine.InputSystem;

namespace Game.Services.InputSystem
{
    public class Inputs
    {
        private readonly Controls inputActions;
        public InputAction Click => inputActions.Gameplay.LeftClick;
        public InputAction Escape => inputActions.Gameplay.Escape;
        public InputAction Space => inputActions.Gameplay.Space;

        public Inputs()
        {
            inputActions = new();
            inputActions.Gameplay.Enable();
        }
    }
}