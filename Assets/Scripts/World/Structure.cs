using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public string key;
    public TaskManager taskManager;

    public abstract void WorkOnTheStructure(float amount);


}
