
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
        
        private Vector2 locationTarget =  Vector2.zero;

        private Vector2 centerLoc;
        
        private float patrolTime;
        
        private Rigidbody2D rb;
        
        public void Start()
        {
            centerLoc = transform.position;
            newPatrolPos();
            
            rb =  GetComponent<Rigidbody2D>();
            
            initialize(3,3, Team.ENEMY);
            
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
            bool isAggro = distSquared <= alertDist;
            
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
            
            
            
            diff.Normalize();
            
            diff *= moveSpeed;
            rb.linearVelocity = diff;
            
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
            Gizmos.DrawCube(pos, Vector3.one);
            Gizmos.DrawWireSphere(pos,patrolRadius);
            Gizmos.DrawWireSphere(pos, aggroDist);
        }

        public void OnTriggerEnter(Collider other)
        {
            GameObject obj = other.transform.gameObject;
            EntityLiving living = obj.GetComponent<EntityLiving>();

            if (living == null)
                return;

            living.damage(this);
        }
    }
}