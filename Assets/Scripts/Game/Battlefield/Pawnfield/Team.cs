using System.Collections.Generic;

namespace Game.Gameplay
{
    public class Team<T> where T : BasePawn
    {
        private readonly List<T> members;

        public int CurrentTeamSize { get; private set; }

        public Team()
        {
            members = new List<T>();
        }

        public void AddToTeam(T candidate)
        {
            members.Add(candidate);
        }

        public void RemoveFromTeam(T candidate)
        {
            members.Remove(candidate);
        }
    }
}