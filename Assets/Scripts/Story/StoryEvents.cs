using Entity;
using Environment.Puzzle.Action;
using Item;
using Item.FunctionalItem;
using UnityEngine;
using Yarn.Unity;

namespace Story
{
    
    //this is some hackathon level shit
    //if we had more time I'd frickin actually make good structure but what the actual heck
    public class StoryEvents
    {
        [YarnCommand("changeLight")]
        public static void changeLighting(float intensity)
        {
            Player player = GameObject.FindObjectOfType<Player>();
            GameItemBase light = player.getSecondaryItem();

            Flashlight l;
            if (!(light is Flashlight))
                return;

            l = (Flashlight)light;
            l.setLight(intensity);
        }
        
        [YarnCommand("startEncounter")]
        public static void HordeEvent(string name)
        {
            GameObject encounter = GameObject.Find(name);
            AbstractAction action = encounter.GetComponent<AbstractAction>();
            action.perform();

        }

    }
}