using UnityEngine;

namespace Entity.Enemy
{
    public class Infected: EntityLiving
    {
        private EntityLiving target;

        
        
        [SerializeField]
        private float baseMoveSpeed;
        
        [SerializeField]
        private float aggroDist;
        

        public void Start()
        {
            
        }

        public void FixedUpdate()
        {
            
        }
    }
}