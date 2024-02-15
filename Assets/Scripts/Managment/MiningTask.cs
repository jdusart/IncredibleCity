using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningTask : AbstractTask
{
    public float workToDo;

    public void Initialized(Structure mine, float workToDo)
    {
        this.structure = mine;
        this.workToDo = workToDo;
    }
    public override void StartTask()
    {
        Debug.Log("Start Mining");
    }

    public override void EndTask(Buddy bud)
    {
        Debug.Log("End Mining");
        bud.TaskFinished();
    }


    public override bool IsFinished(Buddy bud)
    {
        return workToDo < 0;
    }

    public override void Work(Buddy bud)
    {
        workToDo -= Time.deltaTime;
        structure.WorkOnTheStructure(Time.deltaTime);
    }
}
