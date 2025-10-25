using System;

namespace Entity
{
    public abstract class EntityLiving: GameEntity
    {
        protected int health;
        protected int maxHealth;
        protected bool invulnerable;
        protected bool dead;
        
        #nullable enable
        private string team;

        public virtual void initialize(int health, int maxHealth)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.team = null;
        }
    }
}