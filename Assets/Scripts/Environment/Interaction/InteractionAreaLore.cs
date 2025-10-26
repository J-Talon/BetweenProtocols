using Entity;
using UnityEngine;
using Yarn.Unity;

namespace Environment.Interaction
{
    public class InteractionAreaLore: MonoBehaviour, Interactable
    {
        [SerializeField] private DialogueRunner runner;
        [SerializeField] private string node;
        
        public void onInteract(Player player)
        {
            runner.StartDialogue(node);
            Destroy(gameObject);

            //do stuff
        }


        public void OnTriggerEnter(Collider other)
        {

            GameObject obj = other.gameObject;
            Player player = obj.GetComponent<Player>();

            if (player == null)
                return;
            
            onInteract(player);
        }
    }
}