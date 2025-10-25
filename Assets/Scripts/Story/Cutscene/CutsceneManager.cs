using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Story.Cutscene
{
    public class CutsceneManager: MonoBehaviour
    {
        
        /*
         * Ensure that in the game display settings the resolution is 1920x1080
         */
        
        
        public static CutsceneManager instance { get; private set; }

        private Camera cam;
        private Image image;
        private GameObject darkness;
        
        
        private Dictionary<string,Sprite> sprites;

        private Sprite activeSprite = null;

        public void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            
            
            cam = Camera.main;
            DontDestroyOnLoad(cam); 
            darkness = cam.transform.GetChild(0).gameObject;
            darkness.GetComponent<SpriteRenderer>().sortingOrder = -32672;
            
            sprites = new Dictionary<string, Sprite>();
            image = transform.GetChild(0).GetChild(0).GetComponent<Image>();

            clear();
        }
        

        public void playSlideFromResource(string filename)
        {
            Color colour = image.material.color;
            colour.a = 1;
            image.material.color = colour;
            Sprite sprite = Resources.Load<Sprite>(filename);
            if (sprite == null)
            {
                Debug.LogWarning("Cutscene Sprite not found: " + filename);
                return;
            }
            
            sprites.Add(filename, sprite);
            image.sprite = sprite;
        }
        

        public void clear()
        {
            Color colour = image.material.color;
            colour.a = 0;
            image.material.color = colour;
        }

    }
}