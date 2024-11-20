using UnityEngine;

namespace Game.Gameplay
{
    public class PawnFactory
    {
        [Inject] private PawnfieldData pawnfieldData;

        public PlayerPawn CreatePlayer(Vector3 position)
        {
            var view = CreatePawnView(pawnfieldData.PlayerData, position);
            return new PlayerPawn(view);
        }

        public EnemyPawn CreateEnemy(Vector3 position)
        {
            var view = CreatePawnView(pawnfieldData.EnemyData, position);
            return new EnemyPawn(view);
        }

        private Transform CreatePawnView(IPawnData data, Vector3? position)
        {
            return Object.Instantiate(data.PawnObj, position.Value, Quaternion.identity);
        }
    }
}
