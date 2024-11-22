using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public struct CommonerPawnData : IPawnData
    {
        [SerializeField] private Transform pawnObj;

        public readonly Transform PawnObj => pawnObj;
    }
}