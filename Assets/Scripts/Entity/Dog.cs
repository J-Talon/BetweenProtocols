using System;

namespace Entity
{
    public class Dog: EntityLiving
    {

        private Player player;
        public void Start()
        {
            initialize(1,1,Team.PLAYER);
            this.invulnerable = true;
            player = FindObjectOfType<Player>(); //stupid
        }


        public void FixedUpdate()
        {
            
        }
    }
}