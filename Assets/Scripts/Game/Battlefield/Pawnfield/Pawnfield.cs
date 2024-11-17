using UnityEngine;

namespace Game.Gameplay
{
    public sealed class Pawnfield
    {
        private readonly PawnfieldData pawnfieldData;
        private readonly Gamefield gamefield;
        private readonly PawnFactory pawnFactory;

        private readonly Team<PlayerPawn> playerTeam;
        private readonly Team<EnemyPawn> enemyTeam;

        public Team<PlayerPawn> PlayerTeam => playerTeam;
        public Team<EnemyPawn> EnemyTeam => enemyTeam;

        public Pawnfield(PawnfieldData pawnfieldData, Gamefield gamefield, PawnFactory pawnFactory)
        {
            this.pawnfieldData = pawnfieldData;
            this.gamefield = gamefield;
            this.pawnFactory = pawnFactory;

            playerTeam = new(pawnfieldData.PlayerData.PawnCount);
            enemyTeam = new(pawnfieldData.EnemyData.PawnCount);
        }

        public bool TrySpawnAllTeams()
        {
            if (TrySpawnPlayerTeam() && TrySpawnEnemyTeam()) return true;

            return false;
        }

        private bool TrySpawnPlayerTeam()
        {
            var count = pawnfieldData.PlayerData.PawnCount;

            for (int i = 0; i < count; i++)
            {
                var position = pawnfieldData.PlayerData.Position;
                var pawn = SpawnPawn<PlayerPawn>(pawnfieldData.PlayerData, position);
                if (!playerTeam.TryAddToTeam(pawn, i))
                {
                    return false;
                }
            }

            return true;
        }

        private bool TrySpawnEnemyTeam()
        {
            var count = pawnfieldData.EnemyData.PawnCount;

            for (int i = 0; i < count; i++)
            {
                var position = pawnfieldData.EnemyData.Position;
                var pawn = SpawnPawn<EnemyPawn>(pawnfieldData.EnemyData, position);
                if (!enemyTeam.TryAddToTeam(pawn, i))
                {
                    return false;
                }
            }

            return true;
        }

        private T SpawnPawn<T>(IPawnData data, Vector3 position) where T : BasePawn
        {
            return (T)pawnFactory.Create(data, position);
        }
    }
}