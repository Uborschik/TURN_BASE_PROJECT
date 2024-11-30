using Game.Battlefield.Pawnfields;
using Game.Battlefield.States;

namespace Game.Battlefield
{
    public class TurnFactory
    {
        private readonly BattleState battleState;

        public TurnFactory(BattleState battleState)
        {
            this.battleState = battleState;
        }

        public Turn Create(Character character)
        {
            if (character.Owner == Character.OwnerType.Player)
            {
                return new PlayerTurn(battleState, character);
            }

            return new PCTurn(battleState, character);
        }
    }
}