using Game.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class BaseState : State
    {
        protected readonly PlayerPawn Pawn;

        protected float Timer;

        public BaseState(IStateMachine stateMachine, PlayerPawn pawn) : base(stateMachine)
        {
            Pawn = pawn;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            Timer -= Time.deltaTime;
        }
    }
}