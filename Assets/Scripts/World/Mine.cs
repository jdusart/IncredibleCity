using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Structure
{
    public GameObject fill;
    public float work;
    public float workThreshold;
    public GameObject MinePopUp;

    void Start()
    {
        work = 0;
        UpdateGraphics();
    }
    public override void WorkOnTheStructure(float amount)
    {
        work += amount;
        if (work >= workThreshold)
        {
            Debug.Log("Unit done!");
            work = work % workThreshold;
        }
        UpdateGraphics();
    }

    public void AddTask()
    {
        MiningTask task = new MiningTask();
        task.Initialized(this, 3);
        taskManager.AddATask(task);
    }

   
    void UpdateGraphics()
    {
        fill.transform.localScale = new Vector3((work / workThreshold) * 0.98f, 0.7f, 1);
    }

    public void ClosePopUp()
    {
        MinePopUp.SetActive(false);
    }

    private void OnMouseDown()
    {
        MinePopUp.SetActive(true);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse entered");
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse exit");
    }

    private void OnEnable()
    {
        ClosePopUp();
    }
}
