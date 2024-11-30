using System;
using UnityEngine;

namespace Game.Battlefield.Pawnfields.Teams
{
    [Serializable]
    public struct PlayerTeamConfig
    {
        [SerializeField] private CharacterConfig[] characterConfigs;

        public readonly CharacterConfig[] CharacterConfigs => characterConfigs;
    }
}