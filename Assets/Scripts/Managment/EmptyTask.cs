using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTask : AbstractTask
{

    public float waitingTime;
    public EmptyTask(float waitingTime) 
    {
        this.waitingTime = waitingTime;
    }

    public override bool IsFinished(Buddy bud)
    {
        return waitingTime < 0;
    }

   
    public override void Work(Buddy bud)
    {
        waitingTime -= Time.deltaTime;
    }
    public override void StartTask()
    {
        Debug.Log("Start waiting...");
    }

    public override void EndTask(Buddy bud)
    {
        Debug.Log("Finished waiting");
        bud.TaskFinished();
    }
}
