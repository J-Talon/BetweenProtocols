using Entity;
using UnityEngine;

namespace Item
{
    public abstract class GameItemBase:MonoBehaviour
    {
        
        public abstract void holdTick(Vector2 holdDirection, Vector2 entityTransform, float holdOffset);

    }
}