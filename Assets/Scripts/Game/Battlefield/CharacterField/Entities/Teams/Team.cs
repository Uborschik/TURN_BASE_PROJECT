using System.Collections.Generic;
using System.Linq;

namespace Game.Battlefield.Pawnfields.Teams
{
    public class Team
    {
        protected readonly List<Character> members = new();

        public List<Character> Members => members;
        public int CurrentTeamSize => members.Count;

        public Team(Character[] characters)
        {
            members = characters.ToList();
        }

        public Character GetCharacter(int index)
        {
            return members[index];
        }

        public void AddToTeam(Character candidate)
        {
            members.Add(candidate);
        }

        public void RemoveFromTeam(Character candidate)
        {
            members.Remove(candidate);
        }
    }
}