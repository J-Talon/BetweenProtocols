using Item.FunctionalItem;
using UnityEngine;

namespace Item
{
    public static class ItemFactory
    {

        
        private static GameObject FLASHLIGHT = Resources.Load<GameObject>("Item/Flashlight");
        
        private static GameObject PISTOL = Resources.Load<GameObject>("Item/Pistol");
        
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


        public static Pistol createPistol(Vector2 position)
        {
            if (PISTOL == null)
            {
                Debug.LogError("Pistol not found");
                return null;
            }

            GameObject obj = GameObject.Instantiate(PISTOL, new Vector3(position.x, position.y, 0), Quaternion.identity);
            return obj.GetComponent<Pistol>();

        }



    }
}