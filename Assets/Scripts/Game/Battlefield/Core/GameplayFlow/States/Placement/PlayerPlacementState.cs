using Game.Battlefield.Core;
using UnityEngine;

namespace Game.Battlefield.States
{
    public class PlayerPlacementState : FlowState
    {
        private readonly BattleStageBehaviour battleStage;
        private readonly int characterMaxCount = 4;

        private int currentCharacterCount;

        public PlayerPlacementState(BattleStageBehaviour battleStage)
        {
            this.battleStage = battleStage;
        }

        public override void Enter()
        {
            base.Enter();

            battleStage.EnableInput();
            battleStage.ClickPosition += CreatePlayerCharacter;
        }

        public override void Update()
        {
            if (currentCharacterCount == characterMaxCount) battleStage.NextState();
        }

        public override void Exit()
        {
            base.Exit();

            battleStage.DesableInput();
            battleStage.ClickPosition -= CreatePlayerCharacter;
        }

        private void CreatePlayerCharacter(Vector3 position)
        {
            if (battleStage.CharacterField.TrySetPlayerCharacterPosition(currentCharacterCount, position))
            {
                currentCharacterCount++;
            }
        }
    }
}