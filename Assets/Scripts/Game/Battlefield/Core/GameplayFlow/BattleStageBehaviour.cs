using Game.Battlefield.Core;
using Game.Battlefield.States;
using Game.Gameplay;
using Game.Gameplay.Gamefields;
using Game.Gameplay.Pawnfields.Factories;
using Game.Services.InputSystem;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Game.Battlefield
{
    public class BattleStageBehaviour : IInitializable, IStartable, ITickable
    {
        [Inject] private readonly Inputs input;
        [Inject] private readonly Gamefield gamefield;
        [Inject] private readonly CharacterField characterField;
        [Inject] private readonly CharacterFactory characterFactory;
        [Inject] private readonly Camera currentCamera;

        public Gamefield Gamefield => gamefield;
        public CharacterField CharacterField => characterField;
        public CharacterFactory CharacterFactory => characterFactory;


        public event Action<Vector3> ClickPosition;
        private InputAction Click => input.Click;


        private PlayerPlacementState playerPlacementState;
        private EnemyPlacementState enemyPlacementState;
        private BattleState battleState;

        private FlowStateMachine stateMachine;

        #region Methods

        public void Initialize()
        {
            var states = new FlowState[] {
            enemyPlacementState = new(this),
            playerPlacementState = new(this),
            battleState = new(this) };

            stateMachine = new(false, states);
        }

        public void Start()
        {
            stateMachine.Initialize();
        }

        public void Tick()
        {
            stateMachine.CurrentState.Update();
        }

        public void NextState() => stateMachine.Next();

        public void EnableInput()
        {
            Click.performed += GetWorldPositionOnClick;
        }

        public void DesableInput()
        {
            Click.performed -= GetWorldPositionOnClick;
        }

        private void GetWorldPositionOnClick(InputAction.CallbackContext context)
        {
            var position = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            ClickPosition?.Invoke(position);
        }

        #endregion
    }
}