using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetObj : MonoBehaviour
{
    private Vector3 pos;
    private Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        rot = transform.rotation;
    }

    public void reset()
    {
        transform.position = pos;
        transform.rotation = rot;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
