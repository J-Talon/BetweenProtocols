using System.Collections;
using System.Collections.Concurrent;
using Entity;
using Entity.Enemy;
using EventSystem;
using UnityEngine;
using Util;
using Yarn.Unity;

namespace Environment.Encounter
{
    public class EncounterInfected: MonoBehaviour
    {
        private ConcurrentDictionary< string, GameEntity> entities = new  ConcurrentDictionary< string, GameEntity >();

        private Player player;
        
        [SerializeField] private int amount = 5;
        [SerializeField] private int spawnPeriod = 3;
        [SerializeField] private float distAround = 10;


        [SerializeField] private DialogueRunner runner;
        [SerializeField] private string node;

        private int spawned = 0;
        public void clean()
        {
            foreach (GameEntity entity in entities.Values)
            {
                entity.die();
            }
            entities.Clear();
        }


        public void endEncounter()
        {
            clean();
            if (runner != null && node != null)
                runner.StartDialogue(node);
            
        }


        public void startEncounter()
        {
            EventManager.entityDeathEvent.subscribe(onEntityDeath);
            EventManager.sceneChangeEvent.subscribe(onSceneChange);
            player = FindObjectOfType<Player>();
            StartCoroutine(tick());
        }

        public IEnumerator tick()
        {
            while (spawned < amount)
            {
                spawn();
                spawned++;
                yield return new WaitForSeconds(spawnPeriod);
            }
            
            yield return null;
        }


        public void spawn()
        {
            float x = Random.value - Random.value;
            float y = Random.value - Random.value;
            
            float gx = gameObject.transform.position.x;
            float gy = gameObject.transform.position.y;
            Vector2 v = new  Vector2(x, y);
            v.Normalize();
            v *= distAround;

            v.x += gx;
            v.y += gy;

            Infected i = EntityFactory.createInfected(v, player, true);
            i.aggroMoveSpeed = 1;
            i.baseMoveSpeed = 1;
            i.aggroDist = 999;  // screw it
            i.setHealth(1);
            entities.TryAdd(i.getID(), i);
        }



        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(gameObject.transform.position, distAround);
        }


        
        public void onEntityDeath(EntityLiving e)
        {
            if (entities.ContainsKey(e.getID()))
                entities.TryRemove(e.getID(), out _);
            
            if (entities.IsEmpty)
                endEncounter();
        }

        
        public void onSceneChange(string s)
        {
            EventManager.entityDeathEvent.unsubscribe(onEntityDeath);
            EventManager.sceneChangeEvent.unsubscribe(onSceneChange);
        }
    }
}