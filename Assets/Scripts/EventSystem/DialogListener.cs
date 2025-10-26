using UnityEngine;

namespace EventSystem
{
    public interface DialogListener
    {

        public void subscribe()
        {
            EventManager.dialogStartEvent.subscribe(onDialogStart);
            EventManager.dialogEndEvent.subscribe(onDialogEnd);
        }

        public void unsubscribe()
        {
            EventManager.dialogStartEvent.unsubscribe(onDialogStart);
            EventManager.dialogEndEvent.unsubscribe(onDialogEnd);
        }

        public abstract void onDialogStart(string node);
        
        public abstract void onDialogEnd(string node);
    }
}