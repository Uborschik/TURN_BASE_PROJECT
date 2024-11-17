namespace Game.Services
{
    public interface IState
    {
        abstract void Enter();
        abstract void Update();
        abstract void FixedUpdate();
        abstract void LateUpdate();
        abstract void Exit();
    }
}