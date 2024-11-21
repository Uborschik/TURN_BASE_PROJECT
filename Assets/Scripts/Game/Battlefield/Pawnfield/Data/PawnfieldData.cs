using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public struct PawnfieldData
    {
        [SerializeField] private CommonerPawnData commonerData;

        public readonly CommonerPawnData CommonerData => commonerData;
    }
}