using System;
using EventSystem;
using Input;
using Item;
using Story.Cutscene;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Entity
{
    public class Player: EntityLiving, InputListener, DialogListener
    {

        private Rigidbody2D _rigidbody;
        private Light2D _personalLight;
        
        
        private const float MOVE_SPEED = 5f;
        private Vector2 moveDirection = Vector2.zero;

        private Camera mainCamera;
        private Vector2 mouseWorldPosition;


        [Header("Items")] 
        [SerializeField] public float itemOffsetDist = 0.5f;

        private Animator anim;
        private int facingDirection = 1;
        
        private void Start()
        {
            startControlling();
            
            ((DialogListener)this).subscribe();
            
            _rigidbody = GetComponent<Rigidbody2D>();
            mainCamera = Camera.main;
            mouseWorldPosition = Vector2.zero;
            setSecondaryItem(ItemFactory.createFlashlight(transform.position));
            setPrimaryItem(ItemFactory.createPistol(transform.position));
         
            initialize(1,1, Team.PLAYER);


            anim = gameObject.GetComponent<Animator>();
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
            SceneManager.LoadScene("Scenes/ClosingDie");
        }
        
        


        private void itemProceduralAnimation()
        {
          Vector2 transformCoords = transform.position;
          Vector2 diff = mouseWorldPosition - transformCoords;
          
          diff.Normalize();
          diff.x *= facingDirection;
            
            
            if (base.primary != null)
            {
                base.primary.holdTick(diff, itemOffsetDist);
            }
            
            
            if (base.secondary != null)
            {
                base.secondary.holdTick(diff, itemOffsetDist);
            }
        }

        public void MovementUpdate(Vector2 move)
        {
            float magnitude = move.sqrMagnitude;
            
            if (magnitude == 0)
            {
                anim.SetBool("Movin", false);
            }
            else
            {
               anim.SetBool("Movin", true);
            }
        } 

        private void directionUpdate()
        {
            float diff = mouseWorldPosition.x - transform.position.x;
            
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

        public void FixedUpdate()
        {
            itemProceduralAnimation();
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);

            
            _rigidbody.linearVelocity = moveDirection * MOVE_SPEED;

            directionUpdate();


        }


        public void OnTriggerEnter2D(Collider2D other) {

            GameObject obj = other.gameObject;
            EntityLiving living = obj.GetComponent<EntityLiving>();
            
            if (living == null)
                return;

            if (living.getTeam() == this.getTeam()) {
                return;
            }

            this.damage(living);
        }


     

        public void keyMovementVectorUpdate(Vector2 vector)
        {
            moveDirection = vector;
            MovementUpdate(vector);
        }

        public void mousePositionUpdate(Vector2 mousePosition)
        {
            mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
        }

        public void leftMousePress(float mouseValue)
        {
            if (primary != null)
            {
                anim.SetTrigger("TriggerShoot");
                
                if (primary.canUse())
                    primary.use(this);
            }

        }

        public void leftMouseRelease(float mouseValue)
        {
      
        }

        public void mouseHoldDown(float mouseValue)
        {
          
        }

        
        

        public void onDialogStart(string node)
        {
            stopControlling();
            moveDirection = Vector2.zero;
            anim.SetBool("Movin", false);
            invulnerable = true;
        }

        public void onDialogEnd(string node)
        {
            startControlling();
            invulnerable = false;
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
