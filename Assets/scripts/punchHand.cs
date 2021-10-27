using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punchHand : MonoBehaviour
{
    public OVRHand relatedHand;
    public bool rightHand;
    MeshRenderer r;
    Collider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        r = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (relatedHand.IsTracked)
        {
            r.enabled = false;
            col.enabled = false;
        } else
        {
            r.enabled = true;
            col.enabled = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (rightHand)
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        else
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (rightHand)
            OVRInput.SetControllerVibration(0,0, OVRInput.Controller.RTouch);
        else
            OVRInput.SetControllerVibration(0,0, OVRInput.Controller.LTouch);
    }
}
