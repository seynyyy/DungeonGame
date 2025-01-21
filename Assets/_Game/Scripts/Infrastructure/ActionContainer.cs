using System;

namespace _Game.Scripts.Infrastructure
{
    namespace _Game.Scripts.Infrastructure
    {
        public class ActionContainer<T> where T : Delegate
        {
            public T Action { get; set; }

            public ActionContainer(T action)
            {
                Action = action;
            }
        }
    }
}