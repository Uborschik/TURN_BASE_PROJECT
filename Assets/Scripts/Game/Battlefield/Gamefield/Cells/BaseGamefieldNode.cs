using Game.Gameplay.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class BaseGamefieldNode : IGamefieldNode
    {

        private readonly Vector3Int pivotPosition;
        public Vector3Int PivotPosition => pivotPosition;

        private readonly Vector3 centerPosition;
        public Vector3 CenterPosition => centerPosition;

        private BasePawn content;
        public BasePawn Content => content;

        public bool IsWalkable
        {
            get => content == null;
        }

        private List<IGamefieldNode> neighbours;
        public List<IGamefieldNode> Neighbours
        {
            get => neighbours;
            private set => neighbours = value;
        }

        public BaseGamefieldNode(Vector3Int pivotPosition, Vector3 centerPosition)
        {
            this.pivotPosition = pivotPosition;
            this.centerPosition = centerPosition;
        }

        public void SetContent<T>(T pawn) where T : BasePawn
        {
            content = pawn;
        }

        public void AddNeighbors(List<IGamefieldNode> neighbours)
        {
            Neighbours = neighbours;
        }
    }
}