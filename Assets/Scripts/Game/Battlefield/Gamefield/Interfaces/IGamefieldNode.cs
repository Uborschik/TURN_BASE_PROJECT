using System.Collections.Generic;

namespace Game.Gameplay
{
    public interface IGamefieldNode
    {
        public int PivotX { get; }
        public float CenterX { get; }
        public int PivotY { get; }
        public float CenterY { get; }
        public List<IGamefieldNode> Neighbours { get; }
        public bool IsWalkable { get; }
    }
}
