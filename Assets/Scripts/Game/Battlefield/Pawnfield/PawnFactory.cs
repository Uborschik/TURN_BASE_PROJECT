using Game.Gameplay.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public class PawnFactory
    {
        [Inject] private PawnfieldData pawnfieldData;

        public CommonerPawn CreateCommoner(Vector3 position)
        {
            var view = CreatePawnView(pawnfieldData.CommonerData, position);
            return new CommonerPawn(view);
        }

        private Transform CreatePawnView(IPawnData data, Vector3? position)
        {
            return Object.Instantiate(data.PawnObj, position.Value, Quaternion.identity);
        }
    }
}
