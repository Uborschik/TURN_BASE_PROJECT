using Game.Services.FSM;

namespace Game.Battlefield
{
    public class PawnPlacementState : State
    {
        private readonly BattleStageBehaviour battleStage;

        public PawnPlacementState(FiniteStateMachine stateMachine, BattleStageBehaviour battleStage) : base(stateMachine)
        {
            this.battleStage = battleStage;
        }

        public override void Enter()
        {
            PlayerTurn();
        }

        private async void PlayerTurn()
        {
            await battleStage.Pawnfield.EnemyTurn();

            battleStage.EnableInput();
            battleStage.ClickPosition += battleStage.Pawnfield.SpawnPlayerPawn;
        }

        public override void Exit()
        {
            battleStage.DesableInput();
            battleStage.ClickPosition -= battleStage.Pawnfield.SpawnPlayerPawn;
        }
    }
}