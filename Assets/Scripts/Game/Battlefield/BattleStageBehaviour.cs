using Game.DI;
using Game.Gameplay;
using Game.Services.FSM;
using Game.Services.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Battlefield
{
    public class BattleStageBehaviour : IInitializable, IStartable
    {
        #region InjectedFields

        [Inject] private readonly InputInstaller input;
        [Inject] private readonly Pawnfield pawnfield;
        [Inject] private readonly Camera currentCamera;

        #endregion
        #region InputActions

        public event Action<Vector3> ClickPosition;
        private InputAction Click => input.Click;
        private InputAction Escape => input.Escape;
        public InputAction Space => input.Space;

        #endregion

        public Pawnfield Pawnfield => pawnfield;

        #region FSMRegion

        public PawnPlacementState PlacementState { get; private set; }
        public BattleState BattleState { get; private set; }

        private FiniteStateMachine stateMachine;

        #endregion

        #region ControlsMethods

        public void Initialise()
        {
            PlacementState = new(stateMachine, this);
            BattleState = new(stateMachine, this);

            stateMachine = new();
        }

        public void Start()
        {
            stateMachine.Initialize(PlacementState);
        }

        public void EnableInput()
        {
            Click.performed += GetWorldPositionOnClick;
            Escape.performed += ChangeBattleState;
        }

        public void DesableInput()
        {
            Click.performed -= GetWorldPositionOnClick;
            Escape.performed -= ChangeBattleState;
        }

        #endregion

        private void GetWorldPositionOnClick(InputAction.CallbackContext context)
        {
            var position = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            ClickPosition?.Invoke(position);
        }

        private void ChangeBattleState(InputAction.CallbackContext context) => stateMachine.ChangeState(BattleState);
    }
}