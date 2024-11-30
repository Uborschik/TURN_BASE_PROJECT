using System;
using UnityEngine;

namespace Game.Battlefield.Pawnfields
{
    [Serializable]
    public struct CharacterConfig
    {
        [SerializeField] private ProfessionConfig professionConfig;

        public readonly ProfessionConfig ProfessionConfig => professionConfig;
    }
}
