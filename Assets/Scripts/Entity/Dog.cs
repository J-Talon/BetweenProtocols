using System;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

namespace Entity
{
    public class Dog: EntityLiving
    {

        private Player player;
        private List<Vector2> checkpoints;
        private Rigidbody2D rb;
        private Animator anim;
        private int dir;
        
        public void Start()
        {
            
            initialize(1,1,Team.PLAYER);
            this.invulnerable = true;
            player = FindObjectOfType<Player>(); //stupid
            checkpoints = new List<Vector2>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            
            EventManager.keyboardMoveActionEvent.subscribe(onPlayerMove);
            EventManager.sceneChangeEvent.subscribe(onSceneChange);
            dir = -1;
        }


        public void FixedUpdate()
        {

            if (player == null)
                return;



            if (checkpoints.Count == 0)
                return;

            //20^2
            if (distSquared(gameObject.transform.position, player.transform.position) > 400)
            {
                checkpoints.Clear();
                gameObject.transform.position = player.transform.position;
            }


            Vector2 target = checkpoints[0];
            float dist = distSquared(target, gameObject.transform.position);
            if (dist < 1)
            {
                anim.SetBool("running", false);
                rb.linearVelocity = Vector2.zero;
                checkpoints.RemoveAt(0);
                return;
            }
            
            anim.SetBool("running", true);
            Vector2 pos = gameObject.transform.position;
            Vector2 direction = new Vector2(target.x - pos.x, target.y - pos.y);
            direction.Normalize();
            direction *= 3;

            Vector3 scale = transform.localScale;
            if (direction.x < 0 && dir > 0)
            {
                scale.x *= -1;
                dir = -1;
            }
            else if (direction.x > 0 && dir < 0)
            {
                scale.x *= -1;
                dir = 1;
            }
            transform.localScale = scale;

            rb.linearVelocity = direction;
        }


        public void onSceneChange(string s)
        {
            EventManager.keyboardMoveActionEvent.unsubscribe(onPlayerMove); 
            EventManager.sceneChangeEvent.unsubscribe(onSceneChange);
        }

        public void onPlayerMove(Vector2 direction)
        {
            if (checkpoints.Count == 0)
            {
                checkpoints.Add(player.transform.position);
                return;
            }

            Vector2 lastPos = checkpoints[checkpoints.Count - 1];
            if (distSquared(lastPos, player.transform.position) > 1)
                checkpoints.Add(lastPos);
        }


        public float distSquared(Vector2 a, Vector2 b)
        {
            float x = a.x - b.x;
            float y = a.y - b.y;

            float distSquared = (x * x) + (y * y);
            return distSquared;
        }
    }
}
