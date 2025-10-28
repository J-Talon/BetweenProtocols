using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using EventSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        private static float fixedTime;
        
        

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
            fixedTime = Time.fixedDeltaTime;
            
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
            EventManager.dialogStartEvent.callEvent(sprite.name);
            Time.fixedDeltaTime = 0;
        }
        
        
        
        
        
        /*
         * Do not use this for transitioning out of a cutscene. Use slideClear() instead
         */
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

            EventManager.dialogStartEvent.callEvent(sprite.name);
            Time.fixedDeltaTime = 0;
            
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


        [YarnCommand("next_scene")]
        public static void transitionScene(string name) {
            EventManager.sceneChangeEvent.callEvent(name);
            SceneManager.LoadScene(name);
        }
        
        
        
        
        
        [YarnCommand("slide_clear")]
        public static void clear()
        {
            Color colour = image.material.color;
            colour.a = 0;
            image.material.color = colour;
            activeSprite = null;
            EventManager.dialogEndEvent.callEvent(activeSprite == null ? "" : activeSprite.name);
            Time.fixedDeltaTime = fixedTime;
        }


        [YarnCommand("start_dialogue_fixed")]
        public static void startFixedPosDialogue()
        {
            EventManager.dialogStartEvent.callEvent(activeSprite == null ? "" : activeSprite.name);
        }


        [YarnCommand("end_dialogue_fixed")]
        public static void endFixedPosDialogue()
        {
            EventManager.dialogEndEvent.callEvent(activeSprite == null ? "" : activeSprite.name);
        }

        
        //temporary until we fix the stuff
        [YarnCommand("quit_game")]
        public static void quit()
        {
            Application.Quit();
        }


    }
}
