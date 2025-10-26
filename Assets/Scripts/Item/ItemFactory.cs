using Item.FunctionalItem;
using UnityEngine;

namespace Item
{
    public static class ItemFactory
    {

        
        private static GameObject FLASHLIGHT = Resources.Load<GameObject>("Item/Flashlight");
        
        public static Flashlight createFlashlight(Vector2 position)
        {
            if (FLASHLIGHT == null)
            {
                Debug.LogError("Flashlight not found");
                return null;
            }

            GameObject obj =  GameObject.Instantiate(FLASHLIGHT, new Vector3(position.x, position.y, 0), Quaternion.identity);
            return obj.GetComponent<Flashlight>();
        }
    }
}