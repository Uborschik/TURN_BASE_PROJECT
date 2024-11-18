namespace Game.Services.FSM
{
    public class State
    {
        protected FiniteStateMachine StateMachine;

        public State(FiniteStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }

        public virtual void Exit() { }
    }
}