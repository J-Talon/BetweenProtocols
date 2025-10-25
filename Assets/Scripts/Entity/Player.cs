using System;
using Input;
using Item;
using Story.Cutscene;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Entity
{
    public class Player: EntityLiving, InputListener
    {

        private Rigidbody2D _rigidbody;
        private Light2D _personalLight;
        
        
        private const float MOVE_SPEED = 5f;
        private Vector2 moveDirection = Vector2.zero;

        private Camera mainCamera;
        private Vector2 mouseWorldPosition;


        [Header("Items")] 
        [SerializeField] public float itemOffsetDist = 0.5f;
        
        
        private void Start()
        {
            startControlling();
            _rigidbody = GetComponent<Rigidbody2D>();
            mainCamera = Camera.main;
            mouseWorldPosition = Vector2.zero;
            
            setSecondaryItem(ItemFactory.createFlashlight(transform.position));
        }


        public void setPrimaryItem(GameItemDynamic item)
        {
            if (primary != null)
                GameObject.Destroy(primary);

            if (item == null)
            {
                primary = null;
                return;
            }

            item.gameObject.transform.parent = gameObject.transform;
            primary = item;
        }

        public void setSecondaryItem(GameItemBase item)
        {
            if (secondary != null)
                GameObject.Destroy(secondary);

            if (item == null)
            {
                secondary = null;
                return;
            }

            item.gameObject.transform.parent = gameObject.transform;
            secondary = item;
        }






        public override void die()
        {
            stopControlling();
        }


        private void itemProceduralAnimation()
        {
          Vector2 transformCoords = transform.position;
          Vector2 diff = mouseWorldPosition - transformCoords;
          
          diff.Normalize();
            
            
            if (base.primary != null)
            {
                base.primary.holdTick(diff, transformCoords, itemOffsetDist);
            }
            
            
            if (base.secondary != null)
            {
                base.secondary.holdTick(diff, transformCoords, itemOffsetDist);
            }
        }




        public void FixedUpdate()
        {
            itemProceduralAnimation();
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
            
            
            _rigidbody.linearVelocity = moveDirection * MOVE_SPEED;
            
        }
        
        
        


        public void keyMovementVectorUpdate(Vector2 vector)
        {
            moveDirection = vector;
        }

        public void mousePositionUpdate(Vector2 mousePosition)
        {
            mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
        }

        public void leftMousePress(float mouseValue)
        {
          //  CutsceneManager.instance.playSlide("cutscene-test1_0");
        }

        public void leftMouseRelease(float mouseValue)
        {
      
        }

        public void mouseHoldDown(float mouseValue)
        {
          
        }
        
        
        
        public void stopControlling()
        {
            ((InputListener)this).unsubscribe();
        }

        public void startControlling()
        {
            ((InputListener)this).subscribe();
        }
        
    }
}