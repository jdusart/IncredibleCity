using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    List<AbstractTask> tasks = new List<AbstractTask>();


    public AbstractTask AskTask(Buddy bud)
    {
        if (tasks.Count > 0)
        {
            AbstractTask result = tasks[0];
            tasks.RemoveAt(0);
            return result;
        }
        return new EmptyTask(2.0f);
    }


    public bool AddATask(AbstractTask task)
    {
        if (task is EmptyTask)
            return true;
        tasks.Add(task);
        return true;
    }
    

}
