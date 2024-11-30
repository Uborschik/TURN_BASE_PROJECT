using System;
using UnityEngine;

namespace Game.Battlefield.Pawnfields.Teams
{
    [Serializable]
    public struct EnemyTeamConfig
    {
        [SerializeField] private CharacterConfig[] characterConfigs;

        public readonly CharacterConfig[] CharacterConfigs => characterConfigs;
    }
}