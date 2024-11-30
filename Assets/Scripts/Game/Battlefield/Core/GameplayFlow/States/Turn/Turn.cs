using Game.Battlefield.Core;
using Game.Battlefield.Pawnfields;
using UnityEngine;

namespace Game.Battlefield.States
{
    public abstract class Turn : FlowState
    {
        protected readonly BattleState BattleState;
        protected readonly Character Character;

        private float tTime = 1f;

        public Turn(BattleState battleState, Character character)
        {
            BattleState = battleState;
            Character = character;
        }

        public override void Enter()
        {
            Debug.Log($"Enter to {BattleState.GetType().Name}/{GetType().Name}");
        }

        public override void Update()
        {
            tTime -= Time.deltaTime;
            if (tTime <= 0)
            {
                tTime = 1f;
                BattleState.TurnStateMachine.Next();
            }
        }

        public override void Exit()
        {
            Debug.Log($"Exit from {BattleState.GetType().Name}/{GetType().Name}");
        }
    }
}
