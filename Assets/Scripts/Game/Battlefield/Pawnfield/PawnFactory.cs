using UnityEngine;

namespace Game.Gameplay
{
    public class PawnFactory
    {
        [Inject] private PawnfieldData pawnfieldData;
        public PawnfieldData PawnfieldData => pawnfieldData;

        public BasePawn CreatePlayer(Vector3? position)
        {
            return CreatePawn(pawnfieldData.PlayerData, position);
        }

        public BasePawn CreateEnemy(Vector3? position)
        {
            return CreatePawn(pawnfieldData.EnemyData, position);
        }

        private BasePawn CreatePawn(IPawnData data, Vector3? position)
        {
            return Object.Instantiate(data.PawnObj, position.Value, Quaternion.identity, data.Parent);
        }
    }
}
