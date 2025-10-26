using System;
using Entity;
using UnityEngine;

namespace Item.FunctionalItem
{
    public class Pistol: GameItemDynamic
    {
        private Vector2 facingDirection;

        [SerializeField] private int layerMask = 0;
        
        
        
        public override void use(EntityLiving entity)
        {
            
        //    Physics.Raycast(entity.transform.position, facingDirection, 100,1,);
            
            
        }

        public override void holdTick(Vector2 holdDirection, float holdOffset)
        {
            //
            
        }
    }
}