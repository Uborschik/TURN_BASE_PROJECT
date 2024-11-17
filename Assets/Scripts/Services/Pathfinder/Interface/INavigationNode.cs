using System.Collections.Generic;

namespace Game.Services
{
    public interface INavigationNode : IHeapItem<INavigationNode>
    {
        public int PivotX { get; }
        public float CenterX { get; }
        public int PivotY { get; }
        public float CenterY { get; }
        public int G { get; set; }
        public int H { get; set; }
        public int F { get; }
        public List<INavigationNode> Neighbours { get; }
        public bool IsWalkable { get; }
        public INavigationNode Parent { get; set; }
    }
}