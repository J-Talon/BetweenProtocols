using UnityEngine;
using Yarn.Unity;

namespace Environment.Puzzle.Action
{
    public class ActionDialogue: AbstractAction
    {
        [SerializeField] private string dialogueNode;
        [SerializeField] private DialogueRunner runner;
        public override void perform()
        {
            runner.StartDialogue(dialogueNode);
        }
    }
}