using Entity;
using Environment.Puzzle.Action;
using UnityEngine;

namespace Environment.Interaction
{
    public class InteractionAreaAction: MonoBehaviour, Interactable
    {
        
        [SerializeField] private AbstractAction action;

        public void onInteract(Player player)
        {
            action.perform();
            Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Player p = collision.gameObject.GetComponent<Player>();
            if (p != null)
                onInteract(p);
        }
    }
}