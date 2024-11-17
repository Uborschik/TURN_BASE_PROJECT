using Game.Services;
using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerPawn : BasePawn
    {
        public Func<Vector3, Vector3[]> OnNeedHelp;

        [SerializeField] private PawnMovementData movementData;

        public PawnMovement PawnMovement;
        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }

        private StateMachine stateMachine;

        private void Awake()
        {
            PawnMovement = new(movementData, transform);

            stateMachine = new();

            IdleState = new(stateMachine, this);
            MoveState = new(stateMachine, this);

            stateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            stateMachine.CurrentState.Update();
        }
    }
}