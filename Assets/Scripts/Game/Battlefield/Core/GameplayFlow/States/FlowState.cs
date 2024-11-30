using UnityEngine;

namespace Game.Battlefield.Core
{
    public abstract class FlowState
    {
        public virtual void Enter() { Debug.Log($"Enter to {GetType().Name}"); }
        public virtual void Update() { }
        public virtual void Exit() { Debug.Log($"Exit from {GetType().Name}"); }
    }
}