using UnityEngine;

namespace EventSystem
{
    public static class EventManager
    {
        
        public static EventDispatcher<Vector2> keyboardMoveActionEvent = new EventDispatcher<Vector2>();
        public static EventDispatcher<Vector2> mouseMoveEvent = new EventDispatcher<Vector2>();
        public static EventDispatcher<float> mouseButtonDownEvent = new EventDispatcher<float>();
        public static EventDispatcher<float> mouseButtonUpEvent = new EventDispatcher<float>();
        public static EventDispatcher<float> mouseHoldDownEvent = new EventDispatcher<float>();
        
        public static EventDispatcher<string> dialogStartEvent = new EventDispatcher<string>();
        public static EventDispatcher<string> dialogEndEvent = new EventDispatcher<string>();
    }
}