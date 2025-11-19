using Entity;
using Entity.Enemy;
using UnityEngine;

namespace Util
{
    public static class EntityFactory
    {
        private static GameObject dog = Resources.Load<GameObject>("Entity/Dog");
        public static Dog createDog(Vector2 position)
        {
            GameObject i = GameObject.Instantiate(dog, position, Quaternion.identity);
            return i.GetComponent<Dog>();
        }

        private static GameObject infected = Resources.Load<GameObject>("Entity/Infected");
        public static Infected createInfected(Vector2 position, Player target, bool isAngry)
        {
            GameObject i = GameObject.Instantiate(infected, position, Quaternion.identity);
            Infected enemy = i.GetComponent<Infected>();
            enemy.target = target.gameObject;
            enemy.isMad = isAngry;
            return enemy;
        }

    }
}