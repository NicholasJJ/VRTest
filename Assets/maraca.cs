using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maraca : MonoBehaviour
{
    [SerializeField] float threshhold;
    [SerializeField] float underThreshhold;

    [SerializeField] float cooldown;
    float lastShakeTime;
    private bool shaken;
    [SerializeField] int recordframes;
    Vector3[] velocities;

    Vector3 prevAcceleration;

    int index;


    Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {
        velocities = new Vector3[recordframes];
        prevAcceleration = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocities[index] = transform.position - prevPos;
        Vector3 acceleration = (velocities[index] - velocities[(index+1)%recordframes])/Time.fixedDeltaTime;
        index = (index+1)%recordframes;

        if (Vector3.Angle(prevAcceleration,acceleration) > 100) ready();
        if (!shaken && Vector3.Magnitude(acceleration) >= threshhold) {
            shake();
        }
        if (shaken && Vector3.Magnitude(acceleration) < underThreshhold) {
            ready();
        }
        Debug.Log(acceleration);
        prevAcceleration = acceleration;
        prevPos = transform.position;
    }
    void shake() {
        GetComponent<AudioSource>().Play();
            shaken = true;
            lastShakeTime = Time.time;
    }
    void ready(){
        if (Time.time >= lastShakeTime + cooldown)
            shaken = false;
    }
}
