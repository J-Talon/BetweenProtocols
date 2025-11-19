using Entity;
using UnityEngine;

namespace Environment.Puzzle.Action
{
    public class ActionEncounter: AbstractAction
    {
        
        private Player player;

        private void Start()
        {
            player =  FindObjectOfType<Player>();  //this is stupid but sure whatever
        }
        
        public override void perform()
        {
            
            
            
            
            
        }
    }
}