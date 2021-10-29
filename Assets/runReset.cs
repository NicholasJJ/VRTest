using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runReset : MonoBehaviour
{
    public void run()
    {
        BroadcastMessage("reset");
    }
}
