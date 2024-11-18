using UnityEngine;

namespace Game.Gameplay
{
    public class PawnMovement
    {
        private readonly PawnMovementData movementData;
        private readonly Transform pawn;

        private int index;

        public Vector3[] Path { get; set; }

        public PawnMovement(PawnMovementData movementData, Transform pawn)
        {
            this.movementData = movementData;
            this.pawn = pawn;
        }

        public bool TryMoveTo()
        {
            pawn.transform.position = Vector3.MoveTowards(pawn.transform.position, Path[index], movementData.MovementSpeed * Time.deltaTime);

            if (pawn.transform.position == Path[index])
            {
                index++;

                if (index >= Path.Length)
                {
                    index = 0;
                    return false;
                }
            }

            return true;
        }
    }
}