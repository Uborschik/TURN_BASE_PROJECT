using System.Collections.Generic;

namespace Game.Gameplay
{
    public class BaseGamefieldNode : IGamefieldNode
    {

        private readonly int pivotX;
        public int PivotX => pivotX;

        private readonly float centerX;
        public float CenterX => centerX;

        private readonly int pivotY;
        public int PivotY => pivotY;

        private readonly float centerY;
        public float CenterY => centerY;

        private bool isWalkable;
        public bool IsWalkable
        {
            get => isWalkable;
            protected set => isWalkable = value;
        }

        private List<IGamefieldNode> neighbours;
        public List<IGamefieldNode> Neighbours
        {
            get => neighbours;
            private set => neighbours = value;
        }

        public BaseGamefieldNode(int pivotX, float centerX, int pivotY, float centerY)
        {
            this.pivotX = pivotX;
            this.centerX = centerX;
            this.pivotY = pivotY;
            this.centerY = centerY;
        }

        public void AddNeighbors(List<IGamefieldNode> neighbours)
        {
            Neighbours = neighbours;
        }
    }
}