using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public struct PawnfieldData
    {
        [SerializeField] private PlayerPawnData playerData;
        [SerializeField] private EnemyPawnData enemyData;

        public readonly PlayerPawnData PlayerData => playerData;
        public readonly EnemyPawnData EnemyData => enemyData;
    }
}