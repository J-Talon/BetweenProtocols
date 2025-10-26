using EventSystem;
using UnityEngine;

namespace Input
{
    public interface InputListener
    {

        //In order to prevent initialization errors subscribe should be called in Start(), not Awake()
        public void subscribe()
        {
            EventManager.keyboardMoveActionEvent.subscribe(keyMovementVectorUpdate);
            EventManager.mouseMoveEvent.subscribe(mousePositionUpdate);
            EventManager.mouseButtonDownEvent.subscribe(leftMousePress);
            EventManager.mouseButtonUpEvent.subscribe(leftMouseRelease);
            EventManager.mouseHoldDownEvent.subscribe(mouseHoldDown);
   
        }

        public void unsubscribe()
        {
            EventManager.keyboardMoveActionEvent.unsubscribe(keyMovementVectorUpdate);
            EventManager.mouseMoveEvent.unsubscribe(mousePositionUpdate);
            EventManager.mouseButtonDownEvent.unsubscribe(leftMousePress);
            EventManager.mouseButtonUpEvent.unsubscribe(leftMouseRelease);
            EventManager.mouseHoldDownEvent.unsubscribe(mouseHoldDown);
        }


        public abstract void keyMovementVectorUpdate(Vector2 vector);
        
        public abstract void mousePositionUpdate(Vector2 mousePosition);
        
        public abstract void leftMousePress(float mouseValue);
        
        public abstract void leftMouseRelease(float mouseValue);

        public abstract void mouseHoldDown(float mouseValue);

    }
}
