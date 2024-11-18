namespace Game.Gameplay
{
    public sealed class Pawnfield
    {
        private readonly Team<PlayerPawn> playerTeam;
        private readonly Team<EnemyPawn> enemyTeam;

        public Team<PlayerPawn> PlayerTeam => playerTeam;
        public Team<EnemyPawn> EnemyTeam => enemyTeam;

        public Pawnfield()
        {
            playerTeam = new();
            enemyTeam = new();
        }
    }
}