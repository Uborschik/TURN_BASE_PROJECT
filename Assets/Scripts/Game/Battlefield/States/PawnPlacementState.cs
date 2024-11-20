using Game.Gameplay;
using Game.Services.FSM;
using System.Threading.Tasks;
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
            PlayerTurn();
        }

        private async void PlayerTurn()
        {
            await EnemyTurn();

            battleStage.EnableInput();
            battleStage.ClickPosition += SpawnPlayerPawn;
        }

        public override void Exit()
        {
            battleStage.DesableInput();
            battleStage.ClickPosition -= SpawnPlayerPawn;
        }

        private void SpawnEnemyPawn(BaseGamefieldNode node)
        {
            var enemyPawn = battleStage.PawnFactory.CreateEnemy(node.CenterPosition);
            battleStage.Pawnfield.EnemyTeam.AddToTeam(enemyPawn);
            node.SetContent(enemyPawn);
        }

        private void SpawnPlayerPawn(Vector3 position)
        {
            var node = GetSpawnNode(position);

            if (node != null && node.IsWalkable)
            {
                var playerPawn = battleStage.PawnFactory.CreatePlayer(node.CenterPosition);
                battleStage.Pawnfield.PlayerTeam.AddToTeam(playerPawn);
                node.SetContent(playerPawn);
            }
        }

        private async Task EnemyTurn()
        {
            await Task.Delay(2000);

            foreach (var item in GetSpawnPositions())
            {
                SpawnEnemyPawn(item);

                await Task.Delay(1000);
            }

            await Task.Yield();
        }

        private BaseGamefieldNode[] GetSpawnPositions()
        {
            return battleStage.Gamefield.GetRandomNodesAtRightGridSideOnEmptyNodes(3);
        }

        private BaseGamefieldNode GetSpawnNode(Vector3 position)
        {
            return battleStage.Gamefield.GetNodeAtWorldPosition(position);
        }
    }
}