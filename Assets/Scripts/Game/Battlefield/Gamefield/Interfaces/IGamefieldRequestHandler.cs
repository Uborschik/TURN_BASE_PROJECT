using UnityEngine;

namespace Game.Gameplay
{
    public interface IGamefieldRequestHandler
    {
        bool TryGetGamefieldNodeCenterPositionAt(Vector3 position, out Vector3 center);
    }
}
