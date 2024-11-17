using Game.Services;
using UnityEngine;

namespace Game.Gameplay
{
    public class IdleState : BaseState
    {
        public IdleState(IStateMachine stateMachine, PlayerPawn pawn) : base(stateMachine, pawn)
        {
        }

        public override void Enter()
        {
            Timer = 5f;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            base.Update();

            if (Timer <= 0)
            {
                try
                {
                    Pawn.PawnMovement.Path = Pawn.OnNeedHelp?.Invoke(Pawn.transform.position);
                }
                catch (System.Exception)
                {
                    Debug.Log("error");
                }

                if (Pawn.PawnMovement.Path != null)
                {
                    StateMachine.ChangeState(Pawn.MoveState);
                }
            }
        }
    }
}