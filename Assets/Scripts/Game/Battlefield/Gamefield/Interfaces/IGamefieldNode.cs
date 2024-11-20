using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public interface IGamefieldNode
    {
        public Vector3Int PivotPosition { get; }
        public Vector3 CenterPosition { get; }
        public List<IGamefieldNode> Neighbours { get; }
        public bool IsWalkable { get; }
    }
}
