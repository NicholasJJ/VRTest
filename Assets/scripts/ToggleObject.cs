using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour, func
{
    [SerializeField] GameObject obj;
    public void run()
    {
        obj.SetActive(!obj.activeInHierarchy);
    }
}
