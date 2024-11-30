using System;
using UnityEngine;

namespace Game.Gameplay.Pawnfields
{
    [Serializable]
    public struct CharacterFactoryConfig
    {
        [SerializeField] private int playerCount;
        [SerializeField] private int enemyCount;
        [SerializeField] private Transform view;
        [SerializeField] private Transform parent;

        public readonly int PlayerCount => playerCount;
        public readonly int EnemyCount => enemyCount;
        public readonly Transform View => view;
        public readonly Transform Parent => parent;
    }
}