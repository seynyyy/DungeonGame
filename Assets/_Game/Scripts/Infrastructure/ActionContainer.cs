using System;

namespace _Game.Scripts.Infrastructure
{
    public class ActionContainer<T> where T : Delegate
    {
        public T Action { get; private set; }

        public void Subscribe(T action) => Action = (T)Delegate.Combine(Action, action);

        public void Unsubscribe(T action) => Action = (T)Delegate.Remove(Action, action);
    }
}