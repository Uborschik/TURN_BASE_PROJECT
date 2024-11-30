using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Battlefield.Core
{
    public class FlowStateMachine
    {
        private readonly List<FlowState> states = new();
        private readonly bool isCyclic;

        private int currentIndex = 0;
        public FlowState CurrentState { get; private set; }

        public FlowStateMachine(bool isCyclic, IEnumerable<FlowState> states)
        {
            this.states = states.ToList();
            this.isCyclic = isCyclic;
        }

        public void Initialize()
        {
            CurrentState = states[currentIndex];
            CurrentState.Enter();
        }

        public void Next()
        {
            CurrentState.Exit();

            currentIndex = GetNextIndex();

            CurrentState = states[currentIndex];
            CurrentState.Enter();
        }

        private int GetNextIndex()
        {
            if (isCyclic) return (currentIndex + 1) % states.Count;

            var index = ++currentIndex;

            if (index == states.Count)
            {
                throw new Exception("Index out of range!");
            }

            return index;
        }

        public void Remove(int index)
        {
            if (currentIndex == index)
                throw new Exception($"The involved state cannot be removed!");

            states.RemoveAt(currentIndex);
        }
    }
}