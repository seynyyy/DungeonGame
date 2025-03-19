namespace _Game.Scripts.Infrastructure.Finite_State_Machine
{
    public  interface IState
    {
    }
    
    public interface IUpdateState : IState
    {
        void Update();
    }
    
    public interface IEnterState : IState
    {
        void Enter();
    }

    public interface IExitState : IState
    {
        void Exit();
    }
}