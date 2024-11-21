using UnityEngine;

namespace Game.Gameplay
{
    public class PawnMovement
    {
        private readonly Transform pawn;

        private int index;

        public Vector3[] Path { get; set; }

        public PawnMovement(Transform pawn)
        {
            this.pawn = pawn;
        }

        public bool TryMoveTo()
        {
            pawn.transform.position = Vector3.MoveTowards(pawn.transform.position, Path[index], 3 * Time.deltaTime);

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