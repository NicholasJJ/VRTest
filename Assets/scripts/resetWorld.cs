using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetWorld : MonoBehaviour, func
{
    public void run()
    {
        GameObject.Find("world").GetComponent<runReset>().run();
    }
}
