using System;

namespace _Game.Scripts.Infrastructure.Finite_State_Machine
{
    public class Transition<TFrom, TTo> : ITransition where TFrom : IState where TTo : IState
    {
        public bool CanTranslate() => _condition();
        
        private readonly Func<bool> _condition;

        public Transition(Func<bool> condition)
        {
            _condition = condition;
            To = typeof(TTo);
        }

        public Type To { get; }

        public bool CanTranslate(IState fromState) =>
            fromState is TFrom && _condition();

    }
}