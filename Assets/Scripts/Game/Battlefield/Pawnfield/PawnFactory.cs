using UnityEngine;

namespace Game.Gameplay
{
    public class PawnFactory
    {
        public BasePawn Create(IPawnData data, Vector3 position)
        {
            return Object.Instantiate(data.PawnObj, position, Quaternion.identity, data.Parent);
        }
    }
}
