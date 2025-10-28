
using System.Collections;
using Entity;
using Environment.Sound;
using UnityEngine;

namespace Item.FunctionalItem
{
    public class Pistol: GameItemDynamic
    {
        private Vector2 facingDirection;
        private float lastFireTime;
        private float holdOffset;

        [SerializeField] private float useCooldownMillis = 750f;
        [SerializeField] private float maxDist = 100f;
        [SerializeField] private Material bulletDrawLine;


        public override bool canUse()
        {
            float gameTimeMillis = Time.fixedTime * 1000f;
            float diff = gameTimeMillis -  lastFireTime;
            if (diff >= useCooldownMillis)
            {
                return true;
            }

            return false;
        }

        public override void use(EntityLiving entity)
        {
            
            Vector2 pos = entity.transform.position;
            Vector3 source = new Vector3(pos.x, pos.y, 0);
            Vector3 direction = new Vector3(facingDirection.x, facingDirection.y, 0);


            if (!canUse())
                return;
            
            
            lastFireTime = Time.fixedTime * 1000f;

            bool res = SoundManager.instance.playSound("shot");
            Debug.Log(res);



            EntityLiving living = null;
            RaycastHit2D[] hits = Physics2D.RaycastAll(source, direction, maxDist);
            
            foreach (RaycastHit2D hit in hits)
            {
                GameObject obj = hit.transform.gameObject;
                living = obj.GetComponent<EntityLiving>();
                
                if (living == null)
                {
                    break;
                }

                if (living.GetEntityId().Equals(entity.GetEntityId()))
                    continue;
                
                break;
            }

            if (living == null)
                return;
            
            bool damaged = living.damage(entity);
            direction *= 2;
            if (damaged)
                living.push(direction);
        }
        

        public override void holdTick(Vector2 holdDirection, float holdOffset)
        {
            //pistol isn't rendered
            facingDirection = holdDirection;
        }
    }
}