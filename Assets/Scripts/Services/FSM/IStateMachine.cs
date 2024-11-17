namespace Game.Services
{
    public interface IStateMachine
    {
        State CurrentState { get; }

        abstract void Initialize(State startState);
        abstract void ChangeState(State newState);
    }
}