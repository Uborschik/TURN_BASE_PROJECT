using Game.Battlefield.Core;
using Game.Gameplay;
using System.Linq;
using UnityEngine;

namespace Game.Battlefield.States
{
    public class BattleState : FlowState
    {
        private readonly BattleStageBehaviour battleStage;
        private readonly CharacterField characterField;
        private readonly FlowStateMachine turnStateMachine;

        public BattleStageBehaviour BattleStage => battleStage;
        public FlowStateMachine TurnStateMachine => turnStateMachine;

        public BattleState(BattleStageBehaviour battleStage)
        {
            this.battleStage = battleStage;
            characterField = battleStage.CharacterField;

            var turnFactory = new TurnFactory(this);
            var characters = characterField.CalculatePawnQueue();
            var turns = characters.Select(c => turnFactory.Create(c)).ToList();
            turnStateMachine = new(true, turns);
        }

        public override void Enter()
        {
            base.Enter();

            battleStage.EnableInput();
            turnStateMachine.Initialize();
        }

        public override void Update()
        {
            turnStateMachine.CurrentState.Update();
        }

        public override void Exit()
        {
            base.Exit();

            battleStage.DesableInput();
        }
    }
}