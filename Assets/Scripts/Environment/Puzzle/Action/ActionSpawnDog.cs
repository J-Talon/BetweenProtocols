using Entity;
using UnityEngine;
using Util;

namespace Environment.Puzzle.Action
{
    public class ActionSpawnDog: AbstractAction
    {
        
        public override void perform()
        {
            Player player = GameObject.FindObjectOfType<Player>();
            Dog dog = EntityFactory.createDog(player.transform.position);
            //frickin hell it's like bw all over again
        }
    }
}