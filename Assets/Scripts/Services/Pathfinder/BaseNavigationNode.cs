using Game.Battlefield.Pawnfields;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services
{
    public class BaseNavigationNode : INavigationNode
    {
        private Character content;
        public Character Content => content;

        private readonly Vector3Int pivotPosition;
        public Vector3Int PivotPosition => pivotPosition;

        private readonly Vector3 centerPosition;
        public Vector3 CenterPosition => centerPosition;
        public int G { get; set; }
        public int H { get; set; }
        public int F => G + H;
        public int HeapIndex { get; set; }
        public bool IsWalkable { get; set; }
        public INavigationNode Parent { get; set; }
        public List<INavigationNode> Neighbours { get; private set; }

        public BaseNavigationNode(Vector3Int pivotPosition, Vector3 centerPosition, bool isWalkable = true)
        {
            this.pivotPosition = pivotPosition;
            this.centerPosition = centerPosition;
            IsWalkable = isWalkable;
        }

        public void SetContent(Character pawn)
        {
            content = pawn;
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