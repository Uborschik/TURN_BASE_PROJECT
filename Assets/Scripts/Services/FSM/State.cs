namespace Game.Services
{
    public class State
    {
        protected IStateMachine StateMachine;

        public State(IStateMachine stateMachine)
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