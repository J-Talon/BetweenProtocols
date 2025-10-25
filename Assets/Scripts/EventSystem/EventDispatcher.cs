using System;

namespace EventSystem
{
    public class EventDispatcher<T>
    {
        private event Action<T> action;

        public void callEvent(T data)
        {
            action?.Invoke(data);
        }

        public void subscribe(Action<T> handler)
        {
            action += handler;
        }

        public void unsubscribe(Action<T> handler)
        {
            action -= handler;
        }
    }
}