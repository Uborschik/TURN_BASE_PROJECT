using UnityEngine;

namespace Game.Gameplay
{
    public abstract class BasePawn
    {
        public Transform PawnView {  get; protected set; }

        protected BasePawn(Transform pawnView)
        {
            PawnView = pawnView;
        }
    }
}