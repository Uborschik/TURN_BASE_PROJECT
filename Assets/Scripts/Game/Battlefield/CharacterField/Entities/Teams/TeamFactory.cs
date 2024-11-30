using Game.Battlefield.Pawnfields;
using Game.Battlefield.Pawnfields.Teams;

namespace Game.Gameplay.Pawnfields.Factories
{
    public class TeamFactory
    {
        public Team Create(Character[] characters)
        {
            return new Team(characters);
        }
    }
}