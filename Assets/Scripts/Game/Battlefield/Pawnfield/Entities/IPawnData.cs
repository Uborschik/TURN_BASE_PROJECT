using UnityEngine;

namespace Game.Gameplay
{
    public interface IPawnData
    {
        int PawnCount { get; }
        Transform Parent { get; }
        BasePawn PawnObj { get; }
        Vector3 Position { get; }
    }
}
