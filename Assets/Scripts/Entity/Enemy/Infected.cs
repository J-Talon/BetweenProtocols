
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity.Enemy
{
    public class Infected: EntityLiving
    {
        [SerializeField] 
        private GameObject target;  //temporary cause we're out of time

        [SerializeField] private float baseMoveSpeed = 1;

        [SerializeField] private float aggroMoveSpeed = 5;

        [SerializeField] private float aggroDist = 10f;

        [SerializeField] private float patrolRadius = 5f;

        [SerializeField] private float patrolSeconds = 10f;
        
        private Vector2 locationTarget = Vector2.zero;

        private Vector2 centerLoc;
        
        private float patrolTime;
        
        private Rigidbody2D rb;

        private Animator anim;
        private int facingDirection = 1;
        private bool isMad;
        
        
        public void Start()
        {
            centerLoc = transform.position;
            newPatrolPos();
            
            rb =  GetComponent<Rigidbody2D>();
            
            initialize(3,3, Team.ENEMY);
            isMad = false;

            anim = gameObject.GetComponent<Animator>();
        }
        
        
        public override void initialize(int health, int maxHealth, Team team)
        {
            this.team = team;
            invulnerable = false;
            this.health = health;
            this.maxHealth = maxHealth;
            dead = false;
        }

        public void FixedUpdate()
        {
            float distSquared = (target.transform.position - transform.position).sqrMagnitude;
            float alertDist = (aggroDist * aggroDist);
            bool isAggro = distSquared <= alertDist || isMad;
            
            float delta = (Time.fixedDeltaTime / Time.timeScale);

            
            float moveSpeed;
            if (isAggro)
            {
                moveSpeed = aggroMoveSpeed;
                locationTarget = target.transform.position;
            }
            else
            {
                patrolTime += delta;
                moveSpeed = baseMoveSpeed;
                
            }
            
            if (!isAggro && patrolTime >= patrolSeconds)
            {
                newPatrolPos();
            }
            
            Vector2 diff = new Vector2(locationTarget.x - transform.position.x, locationTarget.y - transform.position.y);

            bool atLocation = diff.sqrMagnitude < 0.25f;
            
            diff.Normalize();
            
            if (impulse.sqrMagnitude >= 0.1f)
            {
                rb.linearVelocity = impulse;
                impulse *= 0.95f;
                return;
            }

            if (atLocation)
            {
                rb.linearVelocity = Vector2.zero;
                anim.SetBool("Enemy_Movin", false);
                return;
            }

            diff *= moveSpeed;
            rb.linearVelocity = diff;
            anim.SetBool("Enemy_Movin", true);
            
            directionUpdate(diff.x);
            
        }


        private void directionUpdate(float diff)
        {
            
            if (diff < 0 && facingDirection > 0)
            {
                facingDirection = -1;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            else if (diff > 0 && facingDirection < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                facingDirection = 1;
            }
        }

        public void newPatrolPos()
        {
            float x = (Random.value * patrolRadius) - (Random.value * patrolRadius);
            float y = (Random.value * patrolRadius) - (Random.value * patrolRadius);

            Vector2 next = new Vector2(x + centerLoc.x, y + centerLoc.y);
            locationTarget = next;
            patrolTime = 0;
        }


        public void OnDrawGizmos()
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
            Gizmos.DrawWireSphere(pos,patrolRadius);
            Gizmos.DrawWireSphere(pos, aggroDist);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            GameObject obj = other.transform.gameObject;
            EntityLiving living = obj.GetComponent<EntityLiving>();


            if (living == null)
                return;
            
            attack(living);
        }


        public void attack(EntityLiving other, int damage = 1)
        {
            other.damage(this, damage);
        }


        public override void push(Vector2 vector)
        {
            impulse = vector;
            isMad = true;
        }

    }
}
