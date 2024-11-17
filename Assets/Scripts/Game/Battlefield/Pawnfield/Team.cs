using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Gameplay
{
    public class Team<T> where T : BasePawn
    {
        private readonly T[] members;

        public int CurrentTeamSize { get; private set; }

        public Team(int maxTeamSize)
        {
            members = new T[maxTeamSize];
        }

        public bool TryAddToTeam(T candidate, int index)
        {
            if (CurrentTeamSize == members.Length) return false;

            if (members[index] == null)
            {
                members[index] = candidate;
                CurrentTeamSize++;
                return true;
            }

            return false;
        }

        public bool TryRemoveFromTeam(T candidate)
        {
            if (CurrentTeamSize == 0) return false;

            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] == candidate)
                {
                    members[i] = null;
                    CurrentTeamSize--;
                    return true;
                }
            }

            return false;
        }
    }
}