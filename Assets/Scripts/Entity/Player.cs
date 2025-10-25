using System;
using Input;
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
        
        
        private void Start()
        {
            startControlling();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public override void die()
        {
            stopControlling();
        }


        public void FixedUpdate()
        {
            
        }


        public void keyMovementVectorUpdate(Vector2 vector)
        {
            _rigidbody.linearVelocity = vector;
        }

        public void mousePositionUpdate(Vector2 mousePosition)
        {
         
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