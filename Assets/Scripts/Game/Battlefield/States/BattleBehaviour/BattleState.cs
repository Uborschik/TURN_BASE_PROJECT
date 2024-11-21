using Game.Gameplay.Entities;
using Game.Services.FSM;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Battlefield
{
    public class BattleState : State
    {
        private readonly BattleStageBehaviour battleStage;
        private readonly BattleCyclic battleCyclic;

        public BattleState(FiniteStateMachine stateMachine, BattleStageBehaviour battleStage) : base(stateMachine)
        {
            Debug.Log("BattleState: Created");
            this.battleStage = battleStage;
            battleCyclic = new(battleStage.Pawnfield);
        }

        public override void Enter()
        {
            Debug.Log("BattleState: Started");
            battleStage.EnableInput();
            battleStage.Space.performed += battleCyclic.EndTurn;

            battleCyclic.Run();
        }

        public override void Exit()
        {
            battleStage.DesableInput();
            battleStage.Space.performed -= battleCyclic.EndTurn;
        }
    }
}