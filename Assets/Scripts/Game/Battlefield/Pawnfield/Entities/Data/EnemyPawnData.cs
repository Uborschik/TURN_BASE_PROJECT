using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public struct EnemyPawnData : IPawnData
    {
        [SerializeField, Range(1, 20)] private int pawnCount;

        [SerializeField] private Transform parent;
        [SerializeField] private BasePawn pawnObj;
        [SerializeField] private Vector3 position;

        public readonly int PawnCount => pawnCount;
        public readonly Transform Parent => parent;
        public readonly BasePawn PawnObj => pawnObj;
        public readonly Vector3 Position => position;
    }
}