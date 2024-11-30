namespace Game.Services.StateMachines
{
    public abstract class State
    {
        protected FiniteStateMachine StateMachine;

        public State(FiniteStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Enter();

        public abstract void Exit();
    }
}