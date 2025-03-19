using System;

namespace _Game.Scripts.Infrastructure.Finite_State_Machine
{
    public interface ITransition
    {
        Type To { get; }
        bool CanTranslate(IState fromState);
    }
}