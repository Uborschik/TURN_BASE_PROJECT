using System;
using UnityEngine;

namespace Game.Gameplay.Entities
{
    public abstract class BasePawn
    {
        public Transform PawnView {  get; protected set; }
        public Team Team { get; set; }

        protected BasePawn(Transform pawnView)
        {
            PawnView = pawnView;
        }

        public virtual void Run()
        {
            Debug.Log(PawnView.position);
        }
    }
}