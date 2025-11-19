using Environment.Encounter;
using UnityEngine;

namespace Environment.Puzzle.Action
{
    public class ActionStartEncounter: AbstractAction
    {
        [SerializeField] private EncounterInfected encounter;
        public override void perform()
        {
            if (this.spent)
                return;

            spent = true;
            encounter.startEncounter();
        }
    }
}