using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public struct EnemyPawnData : IPawnData
    {
        [SerializeField] private Transform pawnObj;

        public readonly Transform PawnObj => pawnObj;
    }
}