using UnityEngine;

namespace Environment.Puzzle.Action
{
    public abstract class AbstractAction: MonoBehaviour
    {
        protected bool spent = false;

        public abstract void perform();


    }
}