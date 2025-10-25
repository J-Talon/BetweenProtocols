using UnityEngine;

namespace Entity
{
    public abstract class GameEntity: MonoBehaviour
    {
        protected string guid = null;


        public string getID()
        {
            if (guid == null)
                guid = System.Guid.NewGuid().ToString();
            return guid;
        }


        public virtual void die()
        {
            Destroy(gameObject);
        }
    }
}