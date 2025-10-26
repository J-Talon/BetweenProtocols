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

        protected Team team;

        protected GameItemDynamic primary;
        protected GameItemBase secondary;


        protected void initialize(int health, int maxHealth, Team team)
        {
            this.team = team;
            invulnerable = false;
            this.health = health;
            this.maxHealth = maxHealth;
            dead = false;
        }


        protected enum Team
        {
            PLAYER,
            ENEMY
        };
    }
}