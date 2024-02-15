using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractTask
{
    public UnityAction listenerFinished;

    public Structure structure;


    public abstract void StartTask();
    public abstract void Work(Buddy bud);
    public abstract bool IsFinished(Buddy bud);
    public abstract void EndTask(Buddy bud);

    public Structure GetStructure()
    {
        return structure;
    }

    public void ProcessTask(Buddy bud)
    {
        if (structure!=null && !bud.IsArrivedAt(structure)) {
            bud.Walk(structure);
        }
        else
        {
            if (IsFinished(bud)) 
            {
                EndTask(bud); 
            }
            else
            {
                Work(bud);
            }
        }
    }

    
}
