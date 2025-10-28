using System;
using Item;
using UnityEngine;

namespace Entity
{
    public abstract class EntityLiving: GameEntity
    {
        protected int health;
        protected int maxHealth;
        protected bool invulnerable;
        protected bool dead;

        protected Team team { get; set;  } 

        protected GameItemDynamic primary;
        protected GameItemBase secondary;
        
        protected Vector2 impulse;


        public virtual void initialize(int health, int maxHealth, Team team)
        {
            this.team = team;
            invulnerable = false;
            this.health = health;
            this.maxHealth = maxHealth;
            dead = false;
            impulse = Vector2.zero;
        }


        public bool damage(EntityLiving entity, int damage = 1)
        {
            
            if (invulnerable)
                return false;
            
            if (entity.team == this.team)
                return false;
            
            this.health -= damage;
            if (health <= 0 || dead)
            {
                dead = true;
                die();
            }

            return true;

        }
        
        public virtual void push(Vector2 vector)
        {
            
        }


        public Team getTeam() {
            return team;
        }



        public enum Team
        {
            PLAYER,
            ENEMY
        };
    }
}
