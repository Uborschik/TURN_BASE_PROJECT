using Game.DI;
using Game.Gameplay;
using UnityEngine;

namespace Game.Battlefield
{
    public class BattleStageCyclic : IStartable
    {
        private readonly Pawnfield pawnfield;

        public BattleStageCyclic(Pawnfield pawnfield)
        {
            this.pawnfield = pawnfield;
        }

        public void Start()
        {
            Debug.Log("Is done!");

            if (pawnfield.TrySpawnAllTeams())
            {
                Debug.Log("Is done!");
            }
        }
    }
}