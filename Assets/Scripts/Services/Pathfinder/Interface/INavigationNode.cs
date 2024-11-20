using System.Collections.Generic;
using UnityEngine;

namespace Game.Services
{
    public interface INavigationNode : IHeapItem<INavigationNode>
    {
        public Vector3Int PivotPosition { get; }
        public Vector3 CenterPosition { get; }
        public int G { get; set; }
        public int H { get; set; }
        public int F { get; }
        public List<INavigationNode> Neighbours { get; }
        public bool IsWalkable { get; }
        public INavigationNode Parent { get; set; }
    }
}