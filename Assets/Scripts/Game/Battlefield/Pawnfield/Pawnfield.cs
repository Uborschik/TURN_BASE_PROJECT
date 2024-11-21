using Game.Gameplay.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Game.Gameplay
{
    public struct PawnInitiative
    {
        public readonly BasePawn Pawn;
        public readonly int Value;

        public PawnInitiative(BasePawn pawn, int value)
        {
            this.Pawn = pawn;
            this.Value = value;
        }
    }

    public sealed class Pawnfield
    {
        [Inject] private readonly PawnFactory pawnFactory;
        [Inject] private readonly Gamefield gamefield;

        private const int TEAMS_COUNT = 2;
        private readonly List<Team> teams = new();

        public List<Team> Teams => teams;

        public Pawnfield()
        {
            for (int i = 0; i < TEAMS_COUNT; i++)
            {
                teams.Add(new Team());
            }
        }

        public List<BasePawn> CalculatePawnQueue()
        {
            var allPawns = new List<BasePawn>();
            allPawns.AddRange(teams[0].Members);
            allPawns.AddRange(teams[1].Members);
            var initiative = new PawnInitiative[allPawns.Count];

            for (int i = 0; i < initiative.Length; i++)
            {
                initiative[i] = new PawnInitiative(allPawns[i], Roll.GetValue(20));
            }

            return initiative.OrderByDescending(i => i.Value).Select(i => i.Pawn).ToList();
        }

        public void SpawnPlayerPawn(Vector3 position)
        {
            var node = GetSpawnNode(position);

            if (node != null && node.IsWalkable)
            {
                var playerPawn = pawnFactory.CreateCommoner(node.CenterPosition);
                playerPawn.Team = teams[0];
                teams[0].AddToTeam(playerPawn);
                node.SetContent(playerPawn);
            }
        }

        public async Task EnemyTurn()
        {
            await Task.Delay(2000);

            foreach (var item in GetSpawnPositions())
            {
                SpawnEnemyPawn(item);

                await Task.Delay(1000);
            }

            await Task.Yield();
        }

        private void SpawnEnemyPawn(BaseGamefieldNode node)
        {
            var enemyPawn = pawnFactory.CreateCommoner(node.CenterPosition);
            enemyPawn.Team = teams[1];
            teams[1].AddToTeam(enemyPawn);
            node.SetContent(enemyPawn);
        }

        private BaseGamefieldNode[] GetSpawnPositions()
        {
            return gamefield.GetRandomNodesAtRightGridSideOnEmptyNodes(1);
        }

        private BaseGamefieldNode GetSpawnNode(Vector3 position)
        {
            return gamefield.GetNodeAtWorldPosition(position);
        }
    }
}