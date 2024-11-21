using Game.Gameplay.Entities;
using System.Collections.Generic;

namespace Game.Gameplay
{
    public class Team
    {
        private readonly List<BasePawn> members;

        public List<BasePawn> Members => members;
        public int CurrentTeamSize => members.Count;

        public Team()
        {
            members = new List<BasePawn>();
        }

        public void AddToTeam(BasePawn candidate)
        {
            Members.Add(candidate);
        }

        public void RemoveFromTeam(BasePawn candidate)
        {
            Members.Remove(candidate);
        }
    }
}