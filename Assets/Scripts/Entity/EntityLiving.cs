using System;
using Item;

namespace Entity
{
    public abstract class EntityLiving: GameEntity
    {
        protected int health;
        protected int maxHealth;
        protected bool invulnerable;
        protected bool dead;
        
        protected string team;

        protected GameItemDynamic primary;
        protected GameItemBase secondary;

        public virtual void initialize(int health, int maxHealth)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.team = null;
            
            this.primary = null;
            this.secondary = null;
        }
    }
}