using Game.Gameplay;
using Game.Services.FSM;
using UnityEngine;

namespace Game.Battlefield
{
    public class PawnPlacementState : State
    {
        private readonly BattleStageCyclic battleStage;

        public PawnPlacementState(FiniteStateMachine stateMachine, BattleStageCyclic battleStage) : base(stateMachine)
        {
            this.battleStage = battleStage;
        }

        public override void Enter()
        {
            battleStage.EnableInput();
            battleStage.ClickPosition += SpawnPawn;
        }

        public override void Exit()
        {
            battleStage.DesableInput();
            battleStage.ClickPosition -= SpawnPawn;
        }

        private void SpawnPawn(Vector3 position)
        {
            if (battleStage.Gamefield.TryGetCentralPositionAtWorldPosition(position, out var center))
            {
                var playerPawn = battleStage.PawnFactory.CreatePlayer(center);
                battleStage.Pawnfield.PlayerTeam.AddToTeam((PlayerPawn)playerPawn);
            }
        }
    }
}