using System.Linq;
using UnityEngine;

namespace _Game.Scripts.Infrastructure.Finite_State_Machine
{
    public class StateMachine
    {
        private IState _current;
        
        private readonly IState[] _states;
        private readonly ITransition[] _transitions;
        
        public StateMachine(IState[] states, ITransition[] transitions)
        {
            _current = states.First();
            _states = states;
            _transitions = transitions;
        }

        public void Update()
        {
            Debug.Log(_current);
            if (_current is IUpdateState updateState)
                updateState.Update();
            
            foreach (ITransition transition in _transitions)
            {
                if (transition.CanTranslate(_current)){
                    Translate(transition);
                }
            }
        }

        private void Translate(ITransition transition)
        {
             if (_current is IExitState exitState)
                 exitState.Exit();
             
             _current  = _states.First(x => x.GetType() == transition.To);
             
             if (_current is IEnterState enterState)
                 enterState.Enter();
        }
    }
}