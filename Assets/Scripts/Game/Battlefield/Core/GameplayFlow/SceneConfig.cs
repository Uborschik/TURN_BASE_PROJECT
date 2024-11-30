using Game.Battlefield.Pawnfields.Teams;
using System;
using UnityEngine;

namespace Game.Battlefield.Core
{
    [Serializable]
    public struct SceneConfig
    {
        [SerializeField] private PlayerTeamConfig playerTeamConfig;
        [SerializeField] private EnemyTeamConfig enemyTeamConfig;

        public readonly PlayerTeamConfig PlayerTeamConfig => playerTeamConfig;
        public readonly EnemyTeamConfig EnemyTeamConfig => enemyTeamConfig;
    }
}
