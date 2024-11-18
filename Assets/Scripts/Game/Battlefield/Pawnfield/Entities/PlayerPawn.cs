using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerPawn : BasePawn
    {
        public Func<Vector3, Vector3[]> OnNeedHelp;

        [SerializeField] private PawnMovementData movementData;

        public PawnMovement PawnMovement;
    }
}