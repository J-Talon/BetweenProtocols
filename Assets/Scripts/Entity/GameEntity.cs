using Common;
using UnityEngine;

namespace Entity
{
    public abstract class GameEntity: MonoBehaviour, Tagged
    {
        protected string guid = null;


        public string getID()
        {
            if (guid == null)
                guid = ((Tagged)this).createId();
            return guid;
        }


        public virtual void die()
        {
            Destroy(gameObject);
        }
    }
}