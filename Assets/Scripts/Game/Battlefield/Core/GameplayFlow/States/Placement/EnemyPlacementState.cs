using Game.Battlefield.Core;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Battlefield.States
{
    public class EnemyPlacementState : FlowState
    {
        private readonly BattleStageBehaviour battleStage;
        private bool isComplete;

        public EnemyPlacementState(BattleStageBehaviour battleStage)
        {
            this.battleStage = battleStage;
            isComplete = false;
        }

        public override void Enter()
        {
            base.Enter();

            battleStage.EnableInput();
            CreateEnemyCharacter();
        }

        public override void Update()
        {
            if (isComplete) battleStage.NextState();
        }

        public override void Exit()
        {
            base.Exit();

            battleStage.DesableInput();
        }

        private void CreateEnemyCharacter()
        {
            battleStage.CharacterField.SetEnemyCharacterPosition();
            isComplete = true;
        }
    }
}
