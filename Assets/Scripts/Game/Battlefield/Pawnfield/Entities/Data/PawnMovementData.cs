using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public struct PawnMovementData
    {
        [SerializeField] private float movementSpeed;

        public readonly float MovementSpeed => movementSpeed;
    }
}