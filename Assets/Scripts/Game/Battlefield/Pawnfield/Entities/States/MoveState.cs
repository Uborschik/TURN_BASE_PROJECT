using Game.Services;
using UnityEngine;

namespace Game.Gameplay
{
    public class MoveState : BaseState
    {
        public MoveState(IStateMachine stateMachine, PlayerPawn pawn) : base(stateMachine, pawn)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter to MoveState!");
        }

        public override void Exit()
        {
            Pawn.PawnMovement.Path = null;
            Debug.Log("Exit from MoveState!");
        }

        public override void Update()
        {
            if (!Pawn.PawnMovement.TryMoveTo())
            {
                StateMachine.ChangeState(Pawn.IdleState);
            }
        }
    }
}