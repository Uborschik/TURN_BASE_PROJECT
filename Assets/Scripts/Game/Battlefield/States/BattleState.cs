using Game.Services.FSM;
using UnityEngine;

namespace Game.Battlefield
{
    public class BattleState : State
    {
        private readonly BattleStageCyclic battleStage;

        public BattleState(FiniteStateMachine stateMachine, BattleStageCyclic battleStage) : base(stateMachine)
        {
            this.battleStage = battleStage;
            this.battleStage = battleStage;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}