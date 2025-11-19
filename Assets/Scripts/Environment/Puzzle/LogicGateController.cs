using System.Collections.Generic;
using Environment.Puzzle.Action;
using UnityEngine;

//AND cause frick it
public class LogicGateController : MonoBehaviour
{
    private List<Lever> levers = new List<Lever>();
    private bool spent = false;
    
    [SerializeField] private bool reusable = false;
    [SerializeField] private AbstractAction action;
    
    public void addLever(Lever lever)
    {
        levers.Add(lever);
    }

    
    //frickin hackathon level stuff
    public void onLeverSwitched(Lever lever)
    {
        foreach (Lever l in levers)
        {
            if (!l.isOn())
                break;
        }

        activate();
    }


    public void activate()
    {
        if (spent && !reusable)
            return;
        
        spent = true;
        action.perform();
    }
}
