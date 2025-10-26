using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Story.Cutscene
{
    public class CutsceneManager: MonoBehaviour
    {
        
        /*
         * Ensure that in the game display settings the resolution is 1920x1080
         */
        
        
        public static CutsceneManager instance { get; private set; }

        private static Camera cam;
        private static Image image;
        private static GameObject darkness;
        
        

        private static Sprite activeSprite = null;

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
            
            image = transform.GetChild(0).GetChild(0).GetComponent<Image>();

            clear();
        }
        
        
        [YarnCommand("slide_change")]
        public static void playSlideFromResource(string filename)
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
            
            image.sprite = sprite;
            activeSprite = sprite;
        }
        


        [YarnCommand("slide_transition")]
        public static IEnumerator transitionSlide(string filename, float transitionSecs)
        {
            Color colour = image.material.color;
            float colourValue = 0;
            const float TRANSITION_SLICE = 0.05f;
            
            Sprite sprite = Resources.Load<Sprite>(filename);
            
            if (sprite == null)
            {
                Debug.Log("Cutscene Sprite not found: " + filename);
                yield break;
            }

            
            float changeDiff =  (transitionSecs * 0.5f) / TRANSITION_SLICE;
            if (changeDiff <= 0)
            {
                image.sprite = sprite;
                activeSprite = sprite;
                yield break;
            }

            float alphaChannelChange = 1 / changeDiff;
            

            if (activeSprite == null)
            {
                colour.a = 0;
                image.material.color = colour;
                image.sprite = sprite;
                
                
                while (colourValue < 1)
                {
                    colourValue += alphaChannelChange;
                    colour.a = colourValue;
                    image.material.color = colour;
                    yield return new WaitForSeconds(TRANSITION_SLICE);
                }

                activeSprite = sprite;
                yield break;
            }



            float colourTransition = 1;
            while (colourTransition > 0)
            {
                colourTransition -= alphaChannelChange;
                colourTransition = Math.Max(0, colourTransition);
                
                colour.r = colourTransition;
                colour.g = colourTransition;
                colour.b = colourTransition;
                image.material.color = colour;
                yield return new WaitForSeconds(TRANSITION_SLICE);
            }

            image.sprite = sprite;
            activeSprite = sprite;
            
            while (colourTransition < 1)
            {
                colourTransition += alphaChannelChange;
                colourTransition = Math.Min(1, colourTransition);
                
                colour.r = colourTransition;
                colour.g = colourTransition;
                colour.b = colourTransition;
                image.material.color = colour;
                yield return new WaitForSeconds(TRANSITION_SLICE);
            }

            yield return null;
        }
        
        
        
        
        
        [YarnCommand("slide_clear")]
        public static void clear()
        {
            Color colour = image.material.color;
            colour.a = 0;
            image.material.color = colour;
            activeSprite = null;
        }

    }
}
