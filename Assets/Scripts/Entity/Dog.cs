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
        
        public void Start()
        {
            
            initialize(1,1,Team.PLAYER);
            this.invulnerable = true;
            player = FindObjectOfType<Player>(); //stupid
            checkpoints = new List<Vector2>();
            rb = GetComponent<Rigidbody2D>();
            
            EventManager.keyboardMoveActionEvent.subscribe(onPlayerMove);
            EventManager.sceneChangeEvent.subscribe(onSceneChange);
        }


        public void FixedUpdate()
        {
            if (checkpoints.Count == 0)
                return;
            
            Vector2 target = checkpoints[0];
            float dist = distSquared(target, gameObject.transform.position);
            if (dist < 1)
            {
                rb.linearVelocity = Vector2.zero;
                checkpoints.RemoveAt(0);
                return;
            }
            
            Vector2 pos = gameObject.transform.position;
            Vector2 direction = new Vector2(target.x - pos.x, target.y - pos.y);
            direction.Normalize();
            direction *= 3;
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