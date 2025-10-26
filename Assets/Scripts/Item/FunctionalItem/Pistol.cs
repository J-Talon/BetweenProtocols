
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


        private bool canUse()
        {
            float gameTimeMillis = Time.fixedTime * 1000f;
            float diff = gameTimeMillis -  lastFireTime;
            if (diff >= useCooldownMillis)
            {
                lastFireTime = Time.fixedTime * 1000f;
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
            {
            //    Debug.Log("cooldown");
                return;
            }

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
                    Debug.Log("hit: "+obj.name);
                    break;
                }

                if (living.GetEntityId().Equals(entity.GetEntityId()))
                    continue;
                
                break;
            }

            if (living == null)
                return;
            
            bool damaged = living.damage(entity);
            if (damaged)
                Debug.Log("damaged: "+living.name);
        }

        public override void holdTick(Vector2 holdDirection, float holdOffset)
        {
            //pistol isn't rendered
            facingDirection = holdDirection;
        }
    }
}