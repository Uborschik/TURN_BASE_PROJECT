using Game.Battlefield.Pawnfields;
using Game.Gameplay.Gamefields;
using UnityEngine;

namespace Game.Battlefield.States
{
    public class PlayerTurn : Turn
    {
        private readonly Gamefield gamefield;

        public PlayerTurn(BattleState battleState, Character character) : base(battleState, character)
        {
            gamefield = BattleState.BattleStage.Gamefield;
        }

        public override void Enter()
        {
            base.Enter();
            Character.NextTurn += Next;
            BattleState.BattleStage.ClickPosition += Move;
            Character.Start();
        }

        public override void Update()
        {
            Character.Update();
        }

        public override void Exit()
        {
            base.Exit();
            Character.NextTurn -= Next;
            BattleState.BattleStage.ClickPosition -= Move;
        }

        private void Next() { BattleState.TurnStateMachine.Next(); }

        private void Move(Vector3 position)
        {
            var path = gamefield.FindPathBetween(Character.View.position, position);

            Character.MoveTo(path);
        }
    }
}
