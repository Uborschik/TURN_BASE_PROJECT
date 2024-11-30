using System;
using UnityEngine;

namespace Game.Battlefield.Pawnfields
{
    [CreateAssetMenu(fileName = "ProfessionName", menuName = "Professions")]
    public class ProfessionConfig : ScriptableObject
    {
        [SerializeField] private Sprite view;
        [SerializeField] private Transform prefab;

        public Sprite View => view;
        public Transform Prefab => prefab;
    }
}