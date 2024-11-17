using System.Collections.Generic;

namespace Game.Services
{
    public class BaseNavigationNode : INavigationNode
    {

        private readonly int pivotX;
        public int PivotX => pivotX;

        private readonly float centerX;
        public float CenterX => centerX;

        private readonly int pivotY;
        public int PivotY => pivotY;

        private readonly float centerY;
        public float CenterY => centerY;
        public int G { get; set; }
        public int H { get; set; }
        public int F => G + H;
        public int HeapIndex { get; set; }
        public bool IsWalkable { get; set; }
        public INavigationNode Parent { get; set; }
        public List<INavigationNode> Neighbours { get; private set; }

        public BaseNavigationNode(int pivotX, float centerX, int pivotY, float centerY, bool isWalkable = true)
        {
            this.pivotX = pivotX;
            this.centerX = centerX;
            this.pivotY = pivotY;
            this.centerY = centerY;
            IsWalkable = isWalkable;
        }

        public int CompareTo(INavigationNode other)
        {
            var compare = F.CompareTo(other.F);

            if (compare == 0)
            {
                compare = H.CompareTo(other.H);
            }
            return -compare;
        }

        public void AddNeighbors(List<INavigationNode> neighbours)
        {
            Neighbours = neighbours;
        }
    }
}