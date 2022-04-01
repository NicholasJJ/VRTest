using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberHand : MonoBehaviour
{
    [SerializeField] bool isRight;
    [SerializeField] float grabRange;
    [SerializeField] Transform world;

    Vector3 lastPos;
    GameObject grabbed = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0;
        if (isRight) {
            x = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        } else {
            x = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        }
        if (x > 0.5 && grabbed == null) {
            GameObject targ = null;
            float minDist = float.MaxValue;
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("grabbable")) {
                float dist = Vector3.Distance(g.transform.position,transform.position);
                if (dist < minDist) {
                    targ = g;
                    minDist = dist;
                }
            } 
            if (minDist < grabRange) {
                Grab(targ);
            }
        } else if (x < 0.5 && grabbed != null) {
            Release();
        }
        lastPos = transform.position;

        if (grabbed) MoveGrabbed();
    }

    // private void FixedUpdate() {
    //     if (grabbed) MoveGrabbed();
    // }

    void Grab(GameObject target) {
        grabbed = target;
        grabbed.transform.parent = transform.parent;
        grabbed.GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
    }
    void Release() {
        grabbed.transform.parent = world;
        grabbed.GetComponent<Rigidbody>().isKinematic = false;
        grabbed = null;
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;
    }
    

    void MoveGrabbed() {
        // grabbed.GetComponent<Rigidbody>().velocity = (transform.position - grabbed.transform.position) / Time.fixedDeltaTime;
        grabbed.transform.position = transform.position;
        grabbed.transform.localRotation = Quaternion.identity;
    }
}
