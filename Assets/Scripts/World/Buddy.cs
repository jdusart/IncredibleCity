using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public enum BuddyState
{
    SLEEP =0,
    WORK = 1,
    FREE = 2,
}

public class Buddy : MonoBehaviour
{
    public TaskManager taskManager;
    public Clock clock;

    public float walkingSpeed;
    public Structure House;
    public BuddyState[] schedule = new BuddyState[24];
    
    AbstractTask _currentTask;

    private void Start()
    {
        if (clock == null)
        {
            Debug.Log("Error: clock is null");
        }
        else
        {
            clock.hoursListeners += hourRing;
        }
        for (int i=0; i<schedule.Length; i++)
        {
            if (i < 7 || i >= 21) schedule[i] = BuddyState.SLEEP;
            if (i >= 7 && i < 8) schedule[i] = BuddyState.FREE;
            if (i>=8 && i< 19) schedule[i] = BuddyState.WORK;
            if (i >= 19 && i < 21) schedule[i] = BuddyState.FREE;
        }


    }

    // Update is called once per frame
    void Update()
    {
        switch (GetSchedule())
        {
            case BuddyState.SLEEP:
                Sleep();
                break;
            case BuddyState.FREE:
                Free();
                break;
            case BuddyState.WORK:
                Work();
                break;
            default:
                Debug.Log("Unrecognized state " + GetSchedule());
                break;

        }
    }

    private void Sleep()
    {
        StopWorking();
        if (!IsArrivedAt(House))
        {
            Walk(House);
        }
    }

    private void Free()
    {
        StopWorking();
    }

    private void Work()
    {
        if (_currentTask == null)
        {
            _currentTask = taskManager.AskTask(this);
            _currentTask.StartTask();
        }
        _currentTask.ProcessTask(this);
    }

    public void StopWorking()
    {
        if (_currentTask != null)
        {
            taskManager.AddATask(_currentTask);
            _currentTask = null;
        }
    }


    public void TaskFinished()
    {
        _currentTask = null;
    }

    public BuddyState GetSchedule()
    {
        int idx = (int)Mathf.Floor(clock.currentTime / (clock.dayLength / 24));
        return schedule[idx];
    }

    void hourRing()
    {
        int idx = (int) Mathf.Floor(clock.currentTime / (clock.dayLength / 24));
        Debug.Log("Hour ring :" + idx + ". Time for " + schedule[idx]);
    }

    public void Walk(Structure structure)
    {
        Vector3 destination = structure.gameObject.transform.position;
        destination.y = this.transform.position.y;
        destination.z = this.transform.position.z;
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, Time.deltaTime * walkingSpeed);
    }


    public bool IsArrivedAt(Structure structure)
    {
        float distance = Mathf.Abs(structure.gameObject.transform.position.x - this.transform.position.x);
        return distance < 50;
    }

}
